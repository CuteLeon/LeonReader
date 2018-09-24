using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using LeonReader.AbstractSADE;
using LeonReader.Client.DirectUI.Container;
using LeonReader.Common;

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
            RefreshCatalogList();
        }

        #region 扫描目录

        /// <summary>
        /// 刷新目录列表
        /// </summary>
        private void RefreshCatalogList()
        {
            LogUtils.Info("刷新目录列表...");

            ClearCatalog();

            ScanCatalog(Application.StartupPath);
        }

        /// <summary>
        /// 清空目录
        /// </summary>
        private void ClearCatalog()
        {
            foreach (var flowPanel in PanelInTabPage.Values)
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
            foreach (var tabPage in PanelInTabPage.Keys)
            {
                tabPage.Dispose();
            }
            PanelInTabPage.Clear();
        }

        /// <summary>
        /// 使用目录内所有扫描器扫描目录
        /// </summary>
        private void ScanCatalog(string directoryPath)
        {
            LogUtils.Info("使用所有扫描器扫描目录...");

            //遍历符合条件的链接库
            foreach (var assembly in assemblyFactory.CreateAssemblys(
                directoryPath,
                path => path.ToUpper().EndsWith("SADE.DLL")))
            {
                if (assembly == null) continue;

                //遍历程序集内的扫描器
                foreach (var scanner in sadeFactory.CreateScanners(assembly))
                {
                    if (scanner == null) continue;

                    LogUtils.Info($"发现扫描器：{scanner.ASDESource} in {assembly.FullName}");

                    TabPage tabPage = CreateCatalogContainer(scanner.ASDESource);
                    try
                    {
                        scanner.ProcessReport += (s, e) => { tabPage.Text = $"{scanner.ASDESource}-发现：{e.ProgressPercentage}"; Application.DoEvents(); };
                        scanner.ProcessCompleted += Scanner_ProcessCompleted;
                        scanner.Process(tabPage);
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
            CatalogTabControl.TabPages.Add(tabPage);

            FlowLayoutPanel flowPanel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            tabPage.Controls.Add(flowPanel);
            flowPanel.Dock = DockStyle.Fill;

            PanelInTabPage.Add(tabPage, flowPanel);
            return tabPage;
        }

        /// <summary>
        /// 扫描器处理完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scanner_ProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LogUtils.Info($"扫描器处理完成：{(sender as Scanner).ASDESource} {(e.Cancelled ? "(手动取消)" : "")}");

            Scanner scanner = sender as Scanner;
            TabPage tabPage = scanner.Argument as TabPage;

            LoadCatalog(PanelInTabPage[tabPage], scanner.ASDESource);
            tabPage.Text = scanner.ASDESource;

            //扫描完成释放扫描器
            scanner.Dispose();
        }

        /// <summary>
        /// 加载目录
        /// </summary>
        private void LoadCatalog(FlowLayoutPanel flowPanel, string source)
        {
            if (flowPanel == null) throw new ArgumentNullException("flowPanel");

            foreach (var article in articleManager.GetNewArticles(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = cardFactory.CreateLargeCard(article);
                AddCardContainer(flowPanel, cardContainer);
            }

            foreach (var article in articleManager.GetScanedArticle(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = cardFactory.CreateNormalCard(article);
                AddCardContainer(flowPanel, cardContainer);
            }

            foreach (var article in articleManager.GetDownloadedArticles(source))
            {
                if (article == null) continue;

                CardContainer cardContainer = cardFactory.CreateSmallCard(article);
                AddCardContainer(flowPanel, cardContainer);
            }
        }

        /// <summary>
        /// 添加卡片控件
        /// </summary>
        /// <param name="flowPanel"></param>
        private void AddCardContainer(FlowLayoutPanel flowPanel, CardContainer cardContainer)
        {
            if (flowPanel == null)
                throw new ArgumentNullException("flowPanel");
            if (cardContainer == null)
                throw new ArgumentNullException("cardContainer");

            cardContainer.BrowserClick += CardContainer_BrowserClick;
            cardContainer.DeleteClick += CardContainer_DeleteClick;
            cardContainer.LocationClick += CardContainer_LocationClick;
            cardContainer.MainButtonClick += CardContainer_MainButtonClick;
            cardContainer.ReadedClick += CardContainer_ReadedClick;
            cardContainer.TitleClick += CardContainer_TitleClick;

            flowPanel.Controls.Add(cardContainer);
            Application.DoEvents();
        }

        #endregion

    }
}
