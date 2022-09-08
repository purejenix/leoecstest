Потрачено:
 - создание прототипа без использования ECS (~3 часа)
 - ознакомление с LeoEcsLite (~3 ????)
 - переделка с помощью LeoEcsLite (~12 часов)
 - рефактор и "полировка" (~3 часа)

Пистема может полностью работать на "сервере", для этого нужно:
 - скопировать Common, Components, Systems
 - убрать из Playground'a Client-системы
 - раскомментировать PlayerSystem : IEcsInitSystem//, IEcsRunSystem

Примечания:
 - двери сделаны закрывающимися, намерянно, так легче тестировать
 - ресурсы (моделька, анимации, текстуры) и основа скрипта PlayerController взяты из Asset Store
 https://assetstore.unity.com/packages/essentials/starter-assets-third-person-character-controller-196526