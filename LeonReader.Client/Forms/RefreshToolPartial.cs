using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LeonReader.AbstractSADE;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Common;
using LeonReader.Model;

namespace LeonReader.Client
{
    public partial class MainForm
    {

        /// <summary>
        /// 点击工具箱刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityToolContainer_RefreshClick(object sender, EventArgs e)
        {
            CardContainer cardContainer = new CardContainer();

            this.RefreshCatalogList();
        }

        #region 扫描目录

        /// <summary>
        /// 刷新目录列表
        /// </summary>
        private void RefreshCatalogList()
        {
            LogUtils.Info("刷新目录列表...");

            this.ClearCatalogControl();

            this.ScanCatalog(Application.StartupPath);
        }

        /// <summary>
        /// 清空目录控件
        /// </summary>
        private void ClearCatalogControl()
        {
            foreach (var flowPanel in this.TabPage_Panel_Rel.Values)
            {
                if (flowPanel != null)
                {
                    while (flowPanel.Controls.Count > 0)
                    {
                        flowPanel.Controls[0].Dispose();
                    }
                    flowPanel.Dispose();
                }
            }
            foreach (var tabPage in this.TabPage_Panel_Rel.Keys)
            {
                tabPage.Dispose();
            }
            this.TabPage_Panel_Rel.Clear();
            this.Scanner_TabPage_Rel.Clear();
        }

        /// <summary>
        /// 使用目录内所有扫描器扫描目录
        /// </summary>
        private void ScanCatalog(string directoryPath)
        {
            LogUtils.Info("使用所有扫描器扫描目录...");

            //遍历符合条件的链接库
            foreach (var assembly in this.assemblyFactory.CreateAssemblys(
                directoryPath,
                path => path.ToUpper().EndsWith("SADE.DLL")))
            {
                if (assembly == null) continue;
                //TODO: 筛选此程序集内与 Scanner.SADESource 相同的 分析器、下载器、导出器 类型，并由新创建的 CardContainer 容器代理ADE处理器对象

                //遍历程序集内的扫描器
                foreach (var scanner in this.sadeFactory.CreateScanners(assembly))
                {
                    if (scanner == null) continue;

                    LogUtils.Info($"发现扫描器：{scanner.SADESource} in {assembly.FullName}");

                    TabPage tabPage = this.CreateCatalogContainer(scanner.SADESource);
                    try
                    {
                        //记录扫描器关联的容器控件
                        this.Scanner_TabPage_Rel.Add(scanner, tabPage);
                        scanner.ProcessReport += (s, e) =>
                        {
                            tabPage.Text = $"{scanner.SADESource}-发现：{e.ProgressPercentage}";
                            Application.DoEvents();
                        };
                        scanner.ProcessCompleted += this.Scanner_ProcessCompleted;
                        scanner.Process();
                    }
                    catch (Exception ex)
                    {
                        LogUtils.Error($"调用扫描器失败：{ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private TabPage CreateCatalogContainer(string source)
        {
            TabPage tabPage = new TabPage()
            {
                Text = $"{source} - 正在扫描...",
            };
            this.CatalogTabControl.TabPages.Add(tabPage);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            tabPage.Controls.Add(flowPanel);
            flowPanel.Dock = DockStyle.Fill;

            this.TabPage_Panel_Rel.Add(tabPage, flowPanel);
            return tabPage;
        }

        /// <summary>
        /// 扫描器处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scanner_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogUtils.Info($"扫描器处理完成：{(sender as Scanner).SADESource} {(e.Cancelled ? "(手动取消)" : "")}");

            Scanner scanner = sender as Scanner;
            TabPage tabPage = this.Scanner_TabPage_Rel[scanner] as TabPage;

            this.LoadCatalog(this.TabPage_Panel_Rel[tabPage], scanner.SADESource, scanner);
            tabPage.Text = scanner.SADESource;

            //扫描完成释放扫描器
            this.Scanner_TabPage_Rel.Remove(scanner);
            scanner.Dispose();
        }

        /// <summary>
        /// 加载目录
        /// </summary>
        /// <param name="flowPanel"></param>
        /// <param name="source"></param>
        /// <param name="scanner"></param>
        private void LoadCatalog(FlowLayoutPanel flowPanel, string source, Scanner scanner)
        {
            if (flowPanel == null) throw new ArgumentNullException("flowPanel");
            if (scanner == null) throw new ArgumentNullException("scanner");

            List<CardContainer> cardContainers = new List<CardContainer>();

            foreach (var article in this.articleManager.GetNewArticles(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = this.cardFactory.CreateLargeCard(article);
                cardContainers.Add(cardContainer);
            }

            foreach (var article in this.articleManager.GetDownloadedArticle(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = this.cardFactory.CreateNormalCard(article);
                cardContainers.Add(cardContainer);
            }

            foreach (var article in this.articleManager.GetReadedArticles(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = this.cardFactory.CreateSmallCard(article);
                cardContainers.Add(cardContainer);
            }

            if (scanner.AnalyzerType != null)
            {
                //TODO: 记录程序集对象  创建ADE
                foreach (var cardContainer in cardContainers)
                    ;// cardContainer.Analyzer=scanner.
            }

            foreach (var cardContainer in cardContainers)
            {
                this.AddCardContainer(flowPanel, cardContainer);
            }
        }

        /// <summary>
        /// 添加卡片控件
        /// </summary>
        /// <param name="flowPanel"></param>
        /// <param name="cardContainer"></param>
        private void AddCardContainer(FlowLayoutPanel flowPanel, CardContainer cardContainer)
        {
            if (flowPanel == null)
                throw new ArgumentNullException("flowPanel");
            if (cardContainer == null)
                throw new ArgumentNullException("cardContainer");

            cardContainer.BrowserClick += this.CardContainer_BrowserClick;
            cardContainer.DeleteClick += this.CardContainer_DeleteClick;
            cardContainer.LocationClick += this.CardContainer_LocationClick;
            cardContainer.MainButtonClick += this.CardContainer_MainButtonClick;
            cardContainer.ReadedClick += this.CardContainer_ReadedClick;
            cardContainer.TitleClick += this.CardContainer_TitleClick;

            flowPanel.Controls.Add(cardContainer);
            Application.DoEvents();
        }

        #endregion

    }
}
