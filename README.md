# LeonReader
  * 强大的文章爬虫平台，可以在抽象 SADE 继承下快速实现对目标网站文章的扫描器、分析器、下载器、导出器，并提供数据持久服务、丰富的文章管理功能和良好的文章阅读体验。
  * 可以在此平台内快速扩展灵活的文章爬虫功能；

> ## **项目结构图：**

![](https://raw.github.com/CuteLeon/LeonReader/master/Document/LeonReader%20项目结构图.jpg)

> ## **项目结构说明：**

> ### **UI层：**

  * **LeonReader.Client**

    用户接口层，提供用户查看文章目录、分析文章、下载文章、导出文章、阅读文章和操作文章状态的功能入口；

> ### **BIZ层：**

  业务逻辑层，提供具体的业务能力，文章扫描、分析、下载、导出的实现逻辑即在这里完成；

  * **LeonReader.AbstractSADE**

    抽象 SADE 基础项目，提供封装并约束的扫描器、分析器、下载器、导出器抽象基类供子类继承；
    帮助子类在约束下快速实现自定义的文章 SADE 模块开发；

  * **GamerSkySADE**

    游民星空演示 SADE 项目，继承自 LeonReader.AbstractSADE ，实现了完整的文章扫描器、分析器、下载器、导出器模块；

  * **LeonReader.ArticleContentManager**

    文章内容管理器项目，提供完整的数据实体与数据库连接间的交互功能，是SADE与数据库交互的媒介；

> ### **DAL层：**

  * **LeonReader.DataAccess**

    数据访问层，通过 CodeFirst 模式 O/RM 框架实现对数据库的访问，并提供数据库初始种子数据，是最贴近数据库的一层；

> ### **Model层：**

  * **LeonReader.Model**

    数据模型层，定义数据实体的结构模型，并通过 O/RM 框架在实体结构变化时自动更新数据库结构，是最贴近数据特征的一层；


> ### **COM层：**

  * **LeonReader.Common**

    通用组件层，用于集成系统通用的功能组件，包含各种 Helper 和 Utils ；

***
项目内使用的DirectUI框架：

> [LeonDirectUI](https://github.com/CuteLeon/LeonDirectUI "https://github.com/CuteLeon/LeonDirectUI")
