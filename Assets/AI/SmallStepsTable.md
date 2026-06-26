# 小步骤与所属类整理

> 统计范围：`Assets/Scripts` 下当前项目脚本。
> 整理口径：把脚本中的生命周期方法、业务方法、刷新方法、查询方法视为“小步骤”；纯数据类和枚举单独列为数据定义。

## 执行步骤

| 序号 | 小步骤 | 所属类 | 方法 | 触发方式 | 主要作用 | 相关对象/配置 |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | 初始化商店候选卡 | `Flow` | `Start()` | Unity 生命周期 | 从仓库随机取出指定数量的英雄卡，并写入候选卡集合。 | `Warehouse`、`OptionalCards`、`Config.optionalCardCount` |
| 2 | 初始化战斗区格子 | `Battle` | `Start()` | Unity 生命周期 | 启动时构建战斗区格子。 | `Config`、`Slot` |
| 3 | 构建战斗区格子矩阵 | `Battle` | `Build()` | `Start()` / Inspector 右键菜单 `Build` | 按 `battleRow` 和 `battleCol` 实例化战斗区 `Slot`，并设置位置与类型。 | `Config.battleRow`、`Config.battleCol`、`Config.slotConfig`、`SlotType.Battle` |
| 4 | 初始化备战区格子 | `Bench` | `Start()` | Unity 生命周期 | 启动时构建备战区格子。 | `Config`、`Slot` |
| 5 | 构建备战区格子数组 | `Bench` | `Build()` | `Start()` / Inspector 右键菜单 `Build` | 按 `benchCount` 实例化备战区 `Slot`，并设置位置与类型。 | `Config.benchCount`、`Config.slotConfig`、`SlotType.Bench` |
| 6 | 初始化卡池库存 | `Warehouse` | `Awake()` | Unity 生命周期 | 读取仓库配置，把每种英雄类型的卡牌数量写入内部字典。 | `Config.warehouseConfig`、`WarehouseConfig.infos` |
| 7 | 从卡池随机取牌 | `Warehouse` | `RandomTakeout(int count)` | 外部调用 | 展开当前卡池，随机抽取 `count` 张卡，扣减库存并返回英雄类型数组。 | `_cards`、`HeroType[]` |
| 8 | 初始化候选卡默认数组 | `OptionalCards` | `Start()` | Unity 生命周期 | 如果候选卡还没有值，则创建默认长度数组。 | `Config.optionalCardCount` |
| 9 | 设置候选卡并通知变化 | `OptionalCards` | `Set(HeroType[] value)` | 外部调用 | 保存候选卡数组，并触发 `OnChange` 事件通知 UI 刷新。 | `HeroType[]`、`OnChange` |
| 10 | 获取当前候选卡 | `OptionalCards` | `Get()` | 外部调用 | 返回当前候选卡数组。 | `_value` |
| 11 | 初始化商店 UI 卡牌 | `UIShop` | `Start()` | Unity 生命周期 | 设置网格列数，实例化 UI 卡牌，首次刷新，并订阅候选卡变化事件。 | `GridLayoutGroup`、`UICard`、`OptionalCards` |
| 12 | 刷新商店 UI 卡牌 | `UIShop` | `RefreshCards(HeroType[] heroTypes)` | `Start()` / `OptionalCards.OnChange` | 遍历 UI 卡牌，把每个英雄类型显示到对应卡牌上。 | `UICard[]`、`HeroType[]` |
| 13 | 设置单张 UI 卡牌类型 | `UICard` | `SetType(HeroType heroType)` | `UIShop.RefreshCards()` | 根据英雄类型读取颜色，并设置到 UI 图片上。 | `Config.heroConfig`、`Image` |
| 14 | 初始化格子 | `Slot` | `Initialize(SlotConfig config, SlotType type)` | `Battle.Build()` / `Bench.Build()` | 保存格子配置和类型，并刷新为未选中状态。 | `SlotConfig`、`SlotType` |
| 15 | 刷新格子显示 | `Slot` | `Refresh(bool selected)` | `Slot.Initialize()` | 根据是否选中决定使用选中颜色或格子类型颜色。 | `SpriteRenderer`、`SlotConfig` |
| 16 | 获取英雄颜色 | `HeroConfig` | `GetColor(HeroType heroType)` | `UICard.SetType()` | 根据英雄类型查找对应颜色；未找到时返回白色。 | `HeroInfo[]` |
| 17 | 获取格子颜色 | `SlotConfig` | `GetColor(SlotType status)` | `Slot.Refresh()` | 根据格子类型查找对应颜色；未找到时返回白色。 | `SlotInfo[]` |

## 数据定义

| 类型 | 所属类/枚举 | 作用 | 当前内容 |
| --- | --- | --- | --- |
| `ScriptableObject` | `Config` | 项目总配置入口，持有商店数量、棋盘尺寸、备战区数量和各类子配置。 | `optionalCardCount`、`heroConfig`、`warehouseConfig`、`battleRow`、`battleCol`、`benchCount`、`slotConfig` |
| `class` | `HeroConfig` | 英雄颜色配置集合。 | `HeroInfo[] infos`、`GetColor()` |
| `struct` | `HeroInfo` | 单个英雄类型与颜色的映射。 | `heroType`、`heroColor` |
| `class` | `WarehouseConfig` | 卡池中各英雄类型的数量配置。 | `WarehouseInfo[] infos` |
| `struct` | `WarehouseInfo` | 单个英雄类型与卡池数量的映射。 | `heroType`、`cardCount` |
| `class` | `SlotConfig` | 格子类型颜色和选中颜色配置。 | `SlotInfo[] infos`、`selectedColor`、`GetColor()` |
| `struct` | `SlotInfo` | 单个格子类型与颜色的映射。 | `type`、`color` |
| `enum` | `HeroType` | 英雄类型枚举。 | `Warrior`、`Tank`、`Assassin`、`Archer`、`Mage` |
| `enum` | `SlotType` | 格子类型枚举。 | `Bench`、`Battle` |
| `class` | `SlotIdTranslator` | 当前为空类，暂未承载步骤。 | 无 |

## 当前流程链路

| 流程 | 步骤链 |
| --- | --- |
| 商店候选卡初始化 | `Warehouse.Awake()` -> `Flow.Start()` -> `Warehouse.RandomTakeout()` -> `OptionalCards.Set()` |
| 商店 UI 初始化与刷新 | `UIShop.Start()` -> `OptionalCards.Get()` -> `UIShop.RefreshCards()` -> `UICard.SetType()` -> `HeroConfig.GetColor()` |
| 备战区初始化 | `Bench.Start()` -> `Bench.Build()` -> `Slot.Initialize()` -> `Slot.Refresh()` -> `SlotConfig.GetColor()` |
| 战斗区初始化 | `Battle.Start()` -> `Battle.Build()` -> `Slot.Initialize()` -> `Slot.Refresh()` -> `SlotConfig.GetColor()` |


