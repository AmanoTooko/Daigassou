## 0.常规 General

- [x] 升级依赖库
- [x] 界面库切换到SunnyUI，解决自适应字体问题
- [ ] Midi解析切换为Midi处理，删除按键处理-待集成
- [ ] 将自动更新转移至Autoupdate.net
- [ ] 搭建本地测试autoupdate
- [ ] 搭建autoupdate额外参数下发()
- [x] 保持单文件模式
- [ ] 

## 1.独奏模式 | Solo Play Mode

### 1.1 新Midi控制器

- [x] 播放
- [x] 暂停
- [x] 前进
- [x] 后退
- [x] 停止



### 1.2 新的演奏callback



### 1.3 界面控制器

### 

- [ ] 播放
- [ ] 暂停
- [ ] 前进
- [ ] 后退
- [ ] 停止





## 2.合奏模式 | Multiplayer Mode

### 2.1 更新machina库

- [x] 功能mapping至新库
- [x] 小队倒计时
- [x] 合奏助手
- [ ] 小队停止
- [x] 合奏停止
- [ ] 合奏准备？ shift 到DX
- [ ] 通知popup增加

### 2.2 在线乐谱 | Midi Online？

- [ ] MIdi压缩/重编码算法？



## 3.Midi键盘 | Midi Keyboard Device Input

更新键盘控制库

- [x] 链接状态更新

- [x] 按键预览

- [x] 按键采集范围选择

- [x] 按键延迟确认

  经过确认，Midi键盘链接后，造成声音延迟的原因实际为声卡。USB的事件是OK的。

- [x] ~~键盘群控~~ 移动至DX

  需要配合loopmidi分裂键盘才能进行群控，需要多开，需要多实例进程

- [ ] 本地回放功能 移动至春分release？

## 4.试听功能 | Midi Preview

实现全新的播放控制库

- [x] 输出设备选择
- [x] 播放
- [x] 暂停
- [x] 向前
- [x] 向后
- [x] 停止
- [x] 播放单个轨道
- [x] 播放多个轨道
- [x] 异常处理

## 5.设定 | Setting

### 5.1 键位设定界面更新 | Key Binding Form Update

​	从原有界面更新为游戏界面

- [x] 从游戏中截图
- [x] 颜色调整
- [x] 增加电吉他绑定键
- [x] 重置按键
- [x] 按键可读化
- [x] 电吉他按键逻辑实装
- [x] 电吉他按键设定键实装

### 5.2 快捷键依赖库更改 | Hotkey Lib Change

​	使用Event监控？

### 5.3 增加后台演奏

- [x] pid选择界面
- [x] 保存设定，启动时自动选择-
- [x] 按键逻辑修改
- [x] 单进程自动绑定
- [x] 绑定时弹框

## 6.其他功能 | Others

### 6.1 MML解析 | MML File Import

​	移除

​	Removed

### 6.2 歌词功能 | Lyric Poster

​	移除，该功能将转移到Dx版本中

​	Removed, but will be shifted to Daigassou !! Dx Version.

### 6.3 悬浮窗 | Overlay

​	预计春季版本更新

- [ ] 悬浮窗调用
- [ ] 悬浮窗协议设计
- [ ] 悬浮窗网页设计
- [ ] 悬浮窗下载器 CopyFrom ACT