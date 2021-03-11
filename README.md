# TraceMiddleware
小组件 生产Sqlite链路日志 分析接口优化

``` csharp
//注册Db
services.TraceRegister();

//引用中间件
app.UseTraceMiddleware();

```
