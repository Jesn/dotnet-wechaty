# dotnet-wechaty
dotnet wechaty


# docker 
```
docker run --name dotnet-wechaty -e Wechaty_Token="your token" -e TZ="Asia/Shanghai"  -d --restart=always registry.cn-shanghai.aliyuncs.com/wechaty/dotnet-wechaty
```

* docker 运行相关参数

| 参数 | 是否必须 | 描述 |
| :-----| :----: | :----: |
| WECHATY_TOKEN | 是 | wechaty token |
| Wechaty_NAME | 否 | 机器人名称 |
| Wechaty_ENDPOINT |否| hostie server|