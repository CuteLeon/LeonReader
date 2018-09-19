# LeonReader
有趣的阅读器...

> ## 项目结构图：

![image](./Document/LeonReader%20项目结构图.jpg)

> ## 项目结构说明：

> ### UI层：
  负责用户交互，文章阅读；
  引用：BIZ层、COM层；

> ### BIZ层：
  负责具体的业务逻辑，包括文章的扫描器、分析器、下载器、导出器的抽象模型和具体实现；
  引用：DAL层、COM层；

> ### DAL层：
  负责数据实体类的定义和数据库O/RM框架的交互和CodeFirst模式的实现；
  引用：COM层；

> ### COM层：
  通用组件集合，用于集成系统通用的功能组件，如日志服务、文件服务、API服务、网络服务等；
  无引用；

***
项目内使用的DirectUI框架：

> [LeonDirectUI](https://github.com/CuteLeon/LeonDirectUI "https://github.com/CuteLeon/LeonDirectUI")