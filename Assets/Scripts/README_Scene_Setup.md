# Настройка системы переходов между сценами

## Что мы создали:

### 1. **SceneLoader** - основной скрипт для перехода между сценами
- Поддерживает загрузку по имени сцены или индексу
- Автоматически проверяет существование сцены в Build Settings
- Сохраняет данные игрока перед переходом
- Поддерживает чекпоинты

### 2. **SceneController** - управление сценами
- Горячие клавиши для тестирования (F1-F4)
- Автоматическое определение текущей сцены
- Методы для программной загрузки сцен

### 3. **PlayerDataManager** - сохранение данных игрока
- Сохраняет позицию игрока между сценами
- Поддерживает чекпоинты
- Автоматически восстанавливает данные в новой сцене

### 4. **SceneTransitionTester** - простой тестер переходов
- Горячие клавиши N, P, R для тестирования
- UI интерфейс для отображения текущей сцены
- Простое тестирование без настройки триггеров

## Пошаговая настройка:

### Шаг 1: Настройка Build Settings (ОБЯЗАТЕЛЬНО!)
1. Откройте **File → Build Settings**
2. Добавьте все сцены в правильном порядке:
   - **Index 0**: Tutorial
   - **Index 1**: Winter
3. Убедитесь, что сцены добавлены и проиндексированы

### Шаг 2: Настройка сцены Tutorial
1. **Создайте триггер для перехода:**
   - Создайте пустой GameObject "SceneLoader_TutorialToWinter"
   - Добавьте компонент `SceneLoader`
   - Настройте параметры:
     - `sceneNameToLoad`: "Winter"
     - `useSceneIndex`: false
     - `savePlayerData`: true
     - `useCheckpoint`: true
     - `checkpointPosition`: (0, 0, 0) или нужная позиция
   - Добавьте `BoxCollider2D` с галочкой "Is Trigger"
   - Разместите триггер там, где должен происходить переход

2. **Добавьте SceneController:**
   - Создайте пустой GameObject "SceneController"
   - Добавьте компонент `SceneController`
   - Настройте `sceneNames`: ["Tutorial", "Winter"]

3. **Добавьте PlayerDataManager:**
   - Создайте пустой GameObject "PlayerDataManager"
   - Добавьте компонент `PlayerDataManager`

4. **Добавьте SceneTransitionTester (опционально):**
   - Создайте пустой GameObject "SceneTransitionTester"
   - Добавьте компонент `SceneTransitionTester`
   - Включите `showTestUI` для отображения интерфейса

### Шаг 3: Настройка сцены Winter
1. **Создайте триггер для возврата:**
   - Создайте пустой GameObject "SceneLoader_WinterToTutorial"
   - Добавьте компонент `SceneLoader`
   - Настройте параметры:
     - `sceneNameToLoad`: "Tutorial"
     - `useSceneIndex`: false
     - `savePlayerData`: true
     - `useCheckpoint`: true
     - `checkpointPosition`: (0, 0, 0) или нужная позиция
   - Добавьте `BoxCollider2D` с галочкой "Is Trigger"

2. **Добавьте SceneController:**
   - Создайте пустой GameObject "SceneController"
   - Добавьте компонент `SceneController`
   - Настройка аналогична Tutorial

3. **Добавьте PlayerDataManager:**
   - Создайте пустой GameObject "PlayerDataManager"
   - Добавьте компонент `PlayerDataManager`

4. **Добавьте SceneTransitionTester (опционально):**
   - Аналогично Tutorial

### Шаг 4: Настройка игрока
1. **Убедитесь, что у игрока есть:**
   - Тег "Player"
   - Компонент `PlayerMovement`
   - `Rigidbody2D`
   - `Collider2D` (НЕ триггер)

2. **Настройте камеру:**
   - Добавьте компонент `CameraFollow` на камеру
   - Камера автоматически найдет игрока

## Тестирование:

### Горячие клавиши (SceneController):
- **F1** - Следующая сцена
- **F2** - Предыдущая сцена  
- **F3** - Перезагрузить текущую сцену
- **F4** - Показать список сцен

### Горячие клавиши (SceneTransitionTester):
- **N** - Следующая сцена
- **P** - Предыдущая сцена
- **R** - Перезагрузить сцену

### Проверка в консоли:
После настройки в консоли должны появиться сообщения:
- "SceneController: Текущая сцена 'Tutorial' (индекс 0)"
- "SceneLoader: Сцена 'Winter' найдена в Build Settings"
- "PlayerDataManager: Данные игрока сохранены" (при переходе)

## Примеры использования:

### Простой переход по имени сцены:
```csharp
// В SceneLoader
sceneNameToLoad = "Winter"
useSceneIndex = false
```

### Переход по индексу сцены:
```csharp
// В SceneLoader
useSceneIndex = true
sceneIndexToLoad = 1
```

### Программная загрузка сцены:
```csharp
// Из любого скрипта
SceneController sceneController = FindObjectOfType<SceneController>();
sceneController.LoadScene("Winter");
```

### Установка чекпоинта:
```csharp
// Из любого скрипта
PlayerDataManager.SetCheckpoint(new Vector3(10, 5, 0));
```

## Частые проблемы:

1. **"Сцена НЕ найдена в Build Settings"** → Добавьте сцену в File → Build Settings
2. **"Триггер не срабатывает"** → Проверьте тег "Player" и настройки коллайдера
3. **"Данные не сохраняются"** → Убедитесь, что PlayerDataManager добавлен в сцену
4. **"Камера не следует"** → Добавьте компонент CameraFollow на камеру

## Структура файлов:

```
Assets/Scripts/
├── Player/
│   ├── PlayerMovement.cs
│   └── CameraFollow.cs
├── Gameplay/
│   ├── SceneController.cs
│   ├── PlayerDataManager.cs
│   ├── GameSetupChecker.cs
│   └── SceneTransitionTester.cs
└── SceneLoader.cs
```

## Исправленные предупреждения:

✅ **Убраны устаревшие методы:**
- `FindObjectsOfType<T>()` → `FindObjectsByType<T>(FindObjectsSortMode.None)`
- `FindObjectOfType<T>()` → `FindFirstObjectByType<T>()`

✅ **Все предупреждения CS0618 исправлены**

После настройки игрок сможет свободно перемещаться между сценами Tutorial и Winter, а его позиция будет сохраняться и восстанавливаться!
