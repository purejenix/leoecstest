Потрачено:
 - создание прототипа без использования ECS (~3 часа)
 - ознакомление с LeoEcsLite (~3 часа)
 - переделка с помощью LeoEcsLite (~12 часов)
 - рефактор и "полировка" (~3 часа)

Система может полностью работать на "сервере", для этого нужно:
 - скопировать папки Common, Components, Systems
 - убрать из Playground'a Client-системы
 - раскомментировать в PlayerSystem.cs:12 - PlayerSystem : IEcsInitSystem//, IEcsRunSystem

Примечания:
 - двери, намеренно сделаны закрывающимися, так легче тестировать
 - ресурсы (моделька, анимации, текстуры) и основа скрипта PlayerController взяты из Asset Store
 https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526
