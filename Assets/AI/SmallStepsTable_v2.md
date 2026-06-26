# 小步骤与所属类整理 v2

> 统计范围：基于 `Assets/Scripts` 当前脚本，并加入“点击商店卡牌后删除卡牌，并在备战席生成对应英雄”的拆分方案。
> 命名规则：显示类统一加 `View` 后缀；数据/逻辑类不使用 `Model` 命名。

## 类职责调整

| 原类名 | v2 建议类名 | 类型 | 职责 |
| --- | --- | --- | --- |
| `Bench` | `BenchView` | 显示类 | 只负责备战席格子的创建和英雄显示刷新，不负责判断能不能放英雄。 |
| `Battle` | `BattleView` | 显示类 | 只负责战斗区格子的创建和显示。 |
| `Slot` | `SlotView` | 显示类 | 只负责单个格子的颜色、选中状态和格子上英雄显示。 |
| `UICard` | `UICardView` | 显示类 | 只负责单张商店卡牌的显示和点击事件上报。 |
| `UIShop` | `ShopView` | 显示类 | 只负责商店 UI 卡牌的创建、刷新、删除/隐藏。 |
| 新增 | `ShopController` | 逻辑类 | 负责处理购买流程：接收卡牌点击、请求备战席写入、成功后通知商店显示删除卡牌。 |
| 新增 | `BenchData` | 数据类 | 负责记录备战席每个位置的英雄数据，提供查找空位和写入英雄能力。 |
| 新增 | `HeroView` | 显示类 | 负责单个英雄在场景里的显示表现。 |
| 新增 | `HeroData` | 数据类 | 负责表示一个英雄实例的数据，例如 `HeroType`。 |

## 当前已有步骤迁移

| 序号 | 小步骤 | 所属类 | 方法 | 触发方式 | 主要作用 | 备注 |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | 初始化商店候选卡 | `Flow` | `Start()` | Unity 生命周期 | 从仓库随机取出指定数量的英雄卡，并写入候选卡集合。 | 可后续改由 `ShopController` 触发。 |
| 2 | 初始化战斗区格子 | `BattleView` | `Start()` | Unity 生命周期 | 启动时构建战斗区格子。 | 原 `Battle.Start()`。 |
| 3 | 构建战斗区格子矩阵 | `BattleView` | `Build()` | `Start()` / Inspector 右键菜单 `Build` | 按 `battleRow` 和 `battleCol` 实例化战斗区 `SlotView`。 | 原 `Battle.Build()`。 |
| 4 | 初始化备战区格子 | `BenchView` | `Start()` | Unity 生命周期 | 启动时构建备战区格子。 | 原 `Bench.Start()`。 |
| 5 | 构建备战区格子数组 | `BenchView` | `Build()` | `Start()` / Inspector 右键菜单 `Build` | 按 `benchCount` 实例化备战区 `SlotView`。 | 原 `Bench.Build()`。 |
| 6 | 初始化卡池库存 | `Warehouse` | `Awake()` | Unity 生命周期 | 读取仓库配置，把每种英雄类型的卡牌数量写入内部字典。 | 不属于显示类，类名不变。 |
| 7 | 从卡池随机取牌 | `Warehouse` | `RandomTakeout(int count)` | 外部调用 | 随机抽取 `count` 张卡，扣减库存并返回英雄类型数组。 | 不属于显示类，类名不变。 |
| 8 | 初始化候选卡默认数组 | `OptionalCards` | `Start()` | Unity 生命周期 | 如果候选卡还没有值，则创建默认长度数组。 | 数据容器，类名不变。 |
| 9 | 设置候选卡并通知变化 | `OptionalCards` | `Set(HeroType[] value)` | 外部调用 | 保存候选卡数组，并触发变化事件。 | 数据容器，类名不变。 |
| 10 | 获取当前候选卡 | `OptionalCards` | `Get()` | 外部调用 | 返回当前候选卡数组。 | 数据容器，类名不变。 |
| 11 | 初始化商店 UI 卡牌 | `ShopView` | `Start()` | Unity 生命周期 | 设置网格列数，实例化 `UICardView`，首次刷新并订阅候选卡变化。 | 原 `UIShop.Start()`。 |
| 12 | 刷新商店 UI 卡牌 | `ShopView` | `RefreshCards(HeroType[] heroTypes)` | `Start()` / `OptionalCards.OnChange` | 遍历 UI 卡牌，把英雄类型显示到对应卡牌上。 | 原 `UIShop.RefreshCards()`。 |
| 13 | 设置单张 UI 卡牌类型 | `UICardView` | `SetType(HeroType heroType)` | `ShopView.RefreshCards()` | 保存当前 `HeroType`，并按英雄颜色刷新卡牌显示。 | 原 `UICard.SetType()` 需要扩展保存类型。 |
| 14 | 初始化格子显示 | `SlotView` | `Initialize(SlotConfig config, SlotType type)` | `BattleView.Build()` / `BenchView.Build()` | 保存格子显示配置和类型，并刷新为未选中状态。 | 原 `Slot.Initialize()`。 |
| 15 | 刷新格子显示 | `SlotView` | `Refresh(bool selected)` | `SlotView.Initialize()` | 根据是否选中决定使用选中颜色或格子类型颜色。 | 原 `Slot.Refresh()`。 |
| 16 | 获取英雄颜色 | `HeroConfig` | `GetColor(HeroType heroType)` | `UICardView.SetType()` / `HeroView.Initialize()` | 根据英雄类型查找对应颜色。 | 配置类不变。 |
| 17 | 获取格子颜色 | `SlotConfig` | `GetColor(SlotType status)` | `SlotView.Refresh()` | 根据格子类型查找对应颜色。 | 配置类不变。 |

## 新增购买与生成英雄步骤

| 序号 | 小步骤 | 所属类 | 方法建议 | 触发方式 | 主要作用 | 成功/失败结果 |
| --- | --- | --- | --- | --- | --- | --- |
| 18 | 记录卡牌当前英雄类型 | `UICardView` | `SetType(HeroType heroType)` | `ShopView.RefreshCards()` | 除了刷新颜色，还把 `heroType` 保存到字段，供点击时上报。 | 无返回。 |
| 19 | 监听卡牌点击 | `UICardView` | `OnPointerClick(PointerEventData eventData)` 或 `OnClick()` | 玩家点击 UI 卡牌 | 上报当前卡牌索引和 `HeroType`，不直接处理购买。 | 触发 `Clicked` 事件。 |
| 20 | 上报卡牌点击事件 | `UICardView` | `Action<int, HeroType> Clicked` | `OnPointerClick()` | 把“第几张卡、是什么英雄”通知给 `ShopView` 或 `ShopController`。 | 无返回。 |
| 21 | 转发商店卡牌点击 | `ShopView` | `HandleCardClicked(int index, HeroType heroType)` | `UICardView.Clicked` | 作为显示层转发点击事件，不判断备战席容量。 | 触发 `BuyRequested` 事件。 |
| 22 | 接收购买请求 | `ShopController` | `BuyCard(int index, HeroType heroType)` | `ShopView.BuyRequested` | 开始一次购买流程。 | 成功则继续删除 UI；失败则不删除。 |
| 23 | 查找空备战位 | `BenchData` | `TryGetEmptyIndex(out int index)` | `ShopController.BuyCard()` | 在备战席数据中查找第一个空位置。 | 找到返回 `true`，否则返回 `false`。 |
| 24 | 写入备战席英雄数据 | `BenchData` | `TryAddHero(HeroType heroType, out int index)` | `ShopController.BuyCard()` | 找到空位后创建/写入 `HeroData`。 | 成功返回写入位置；满员返回失败。 |
| 25 | 通知备战席数据变化 | `BenchData` | `Action<int, HeroData> HeroAdded` | `TryAddHero()` 成功 | 通知显示层哪个位置新增了哪个英雄。 | `BenchView` 收到后刷新对应格子。 |
| 26 | 接收备战席新增英雄显示 | `BenchView` | `HandleHeroAdded(int index, HeroData hero)` | `BenchData.HeroAdded` | 根据数据变化，在指定备战位显示英雄。 | 无返回。 |
| 27 | 在格子上显示英雄 | `SlotView` | `SetHeroView(HeroView heroView)` | `BenchView.HandleHeroAdded()` | 把生成的英雄显示对象挂到对应格子下。 | 无返回。 |
| 28 | 创建英雄显示对象 | `BenchView` | `CreateHeroView(HeroData hero)` | `HandleHeroAdded()` | 实例化 `HeroView`，并初始化英雄类型、颜色或外观。 | 返回 `HeroView`。 |
| 29 | 初始化英雄显示 | `HeroView` | `Initialize(HeroData hero, HeroConfig config)` | `BenchView.CreateHeroView()` | 根据 `HeroData.heroType` 设置英雄显示。 | 无返回。 |
| 30 | 删除已购买商店卡牌显示 | `ShopView` | `RemoveCard(int index)` | `ShopController.BuyCard()` 成功后 | 删除、隐藏或置空已购买的卡牌显示。 | 无返回。 |
| 31 | 同步候选卡数据 | `OptionalCards` | `RemoveAt(int index)` 或 `ClearAt(int index)` | `ShopController.BuyCard()` 成功后 | 把已购买的候选卡从数据中移除或置空，避免 UI 与数据不一致。 | 触发 `OnChange` 或单独变化事件。 |

## v2 推荐流程链路

| 流程 | 步骤链 |
| --- | --- |
| 点击购买卡牌 | `UICardView.OnPointerClick()` -> `UICardView.Clicked` -> `ShopView.HandleCardClicked()` -> `ShopView.BuyRequested` -> `ShopController.BuyCard()` |
| 写入备战席数据 | `ShopController.BuyCard()` -> `BenchData.TryAddHero()` -> `BenchData.TryGetEmptyIndex()` -> `BenchData.HeroAdded` |
| 刷新备战席显示 | `BenchData.HeroAdded` -> `BenchView.HandleHeroAdded()` -> `BenchView.CreateHeroView()` -> `HeroView.Initialize()` -> `SlotView.SetHeroView()` |
| 删除商店卡牌 | `BenchData.TryAddHero()` 成功 -> `ShopController.BuyCard()` -> `ShopView.RemoveCard()` -> `OptionalCards.RemoveAt()` 或 `OptionalCards.ClearAt()` |
| 备战席满员失败 | `BenchData.TryAddHero()` 失败 -> `ShopController.BuyCard()` 结束 -> `ShopView` 不删除卡牌 |

## 数据定义补充

| 类型 | 类名 | 字段/事件建议 | 作用 |
| --- | --- | --- | --- |
| `class` | `BenchData` | `HeroData[] heroes`、`HeroAdded` | 备战席数据源，负责位置占用和英雄写入。 |
| `class` 或 `struct` | `HeroData` | `HeroType heroType` | 单个英雄实例数据。 |
| `class` | `ShopController` | `ShopView shopView`、`BenchData benchData`、`OptionalCards optionalCards` | 商店购买流程协调者。 |
| `class` | `HeroView` | `SpriteRenderer` 或其他显示组件 | 英雄显示对象。 |
| `class` | `ShopView` | `UICardView[] cards`、`BuyRequested` | 商店显示层。 |
| `class` | `UICardView` | `_heroType`、`index`、`Clicked` | 单张卡牌显示与点击上报。 |
| `class` | `BenchView` | `SlotView[] slots`、`heroViewSample` | 备战席显示层。 |
| `class` | `SlotView` | `SlotType`、`HeroView currentHeroView` | 单个格子的显示状态。 |

## 设计约束

| 约束 | 说明 |
| --- | --- |
| `UICardView` 不直接操作 `BenchData` | 卡牌显示不应该知道备战席规则。 |
| `BenchView` 不判断能不能放英雄 | 它是显示类，只响应 `BenchData` 的结果。 |
| `ShopController` 不直接实例化英雄显示 | 购买流程只写数据，显示由 `BenchView` 处理。 |
| 购买失败不删除卡牌 | 例如备战席满了时，商店卡牌应保持不变。 |
| 删除卡牌和候选卡数据要同步 | 否则 UI 删除了，但 `OptionalCards` 里还保留旧数据。 |

