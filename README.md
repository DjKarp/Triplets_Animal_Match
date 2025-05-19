<p align="center">
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/TripletsAnimalMatch_Logo_200_02.png" alt="Triplets Animal Match Icon" width="256" height="256"/>
</p>

<h1 align="center">Triplets Animal Match — Тестовое задание Unity C# Developer</h1>

<p align="center">
  <b>Прототип казуальной головоломки, выполненный в рамках тестового задания на позицию Unity C# Developer.</b>
</p>
<p align="center">

## 🎯 Задание

Реализовать игру, где:
- Игровое заполняется случайными фигурками (форма + цвет рамки + животное).
- Каждая фигурка имеет кратность 3 (всегда можно собрать тройки).
- Анимация старта: фигурки "сыпятся сверху", учитывается физика (гравитация, столкновения).
- Игрок кликает по фигурке — она перемещается в верхнюю панель.
- Если собраны 3 одинаковых — удаляются.
- Если панель переполнена — проигрыш.
- Если поле очищено — победа.
- Есть кнопка пересбора поля.
- Специальные типы фигурок: липкие, замороженные, тяжёлые, взрывающиеся.

## 🛠 Реализация

- Unity 2022.3.23f1
- C# (MVP архитектура)
- Физика на Rigidbody2D и Collider2D
- Генерация фигурок с учётом кратности
- Работа с UI и анимацией (DOTween, Animator)
- Поддержка эффектов (липкость, заморозка и т.д.)

## ▶️ Видео демонстрация

<p align="Left">  
<b>Смотреть Видеотрейлер на RuTube - Кликни по картинке</b><br/>
  </p>
<p align="center">
  
   [![Смотреть Видео](https://redleggames.com/Games/TripletsAnimalMatch/Screens/TripletsAnimalMatch_Logo_200_01.png)](https://rutube.ru/video/55a5f4282da456650ef68a5144672d17/)
   
</p>

<p align="Left">  
<b>Или посмотреть на Google Drive</b><br/>
</p>

[![Смотреть Видео](https://redleggames.com/Games/TripletsAnimalMatch/Screens/TripletsAnimalMatch_Logo_200_03.png)](https://drive.google.com/file/d/1PtvdDw63gc_22FPsWagBhD_gWeEOmqtf/view?usp=sharing)

## 🎥 Скриншоты

<p align="center">
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_01.png" width="200"/>
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_02.png" width="200"/>
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_03.png" width="200"/>
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_04.png" width="200"/>
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_05.png" width="200"/>
  <img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/Triplets_Animal_Match_Screen_06.png" width="200"/>
</p>

## 📁 Структура проекта
<pre> ```Assets/
├── Animation/          # Анимации кнопки для примера
│
├── FMOD/               # Проект FMOD 
│
├── Prefab/
│   └── ShapeRigidbody/ # Префабы Collider2D для разного типа тайлов
│
├── Scenes/             # Все сцены игры 
│   ├── Bootstrap       # Разгоночная сцена, содержит только загрузочный экран, с неё запускаются все остальные сцены
│   └── Game            # Сцена на корторой создаётся игровое окружение и геймплей
│
├── ScriptableObject/   # Файл настроек игры. (подробности чуть ниже)
│
├── Scripts/
│   ├── Audio/          # Audio Service для звука
│   ├── Environment/    # Поведение / элементы уровня
│   ├── Installers/     # Zenject моно инсталлеры
│   ├── Model/          # Скрипты принадлежащие Model
│   │   └── Tile/       # Tile и его производные. Управляют поведением игровых фишек
│   ├── Presenter/      # Скрипт Presenter - связь между моделью и отображением / инпутом
│   ├── Signals/        # Zenject сигналы
│   └──View/            # Всё что отвечает за вид игры и отображение
│        └── UI/        # Экраны поражения, победы, меню (меню имеет стартовый мультик и описание)
│
├──  Settings/          # Разнообразные настройки для плагинов и физические настройки тайлов
│
├──  StreamingAssets/   # FMOD банки
│
└── Textures/             
│   ├── Animals pack/   # Спрайты голов животных для тайлов
│   ├── Effects/        # Картинки эффектов, которые накладываются на ижображения голов животных, если особый тайл
│   ├── Environment/    
│   │   ├── Background/ # Сгенерированные фоны на уровень. Меняются при каждом запуске.
│   │   ├── Cloud/      # Изображения облаков из которых вывалились зверушки-талисманы
│   │   ├── Mult/       # Картинки для мультика (мини история перед началом игры)
│   │   ├── Stakan/     # Игровое поле, куда падают игровые тайлы
└── └── └── TopPanel/   # Панель, куда собираются тайлы перед возвращением на облако
``` </pre>
---

# Scriptable Object - GameplayData
Содержит настройки для кастомизации игры:

| Поле                    | Описание                              |
|-------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| `MaxCountTiles `        | Максимальное количество тайлов, которые сыпяться из точки спавна (облако) в стакан (игровое поле). В референсе - 63.                       |
| `MatchCountTiles`       | Количество тайлов в экшен баре, которое требуется для их исчезновения. По другому - это количество одинаковых тайлов. По заданию - 3.      |
| `TimeSpawn`             | Интервал с которым спавняться тайлы из облака.                                                                                             |
| `MoveTileTime`          | Время для анимации полёта тайла из места, где по нему щёлкнули, до экшен бара. И это же время на анимацию полёта из бара в облако.         |
| `NumberTilesToUnfreeze` | Количество тайлов, которое нужно отправить в облако, чтобы разморозились Freezed тайлы.                                                    |
| `TileEffectCount`       | Список из названия эффекта особых тайлов - TileEffect и количества их на уровне. Т.е. сколько и каких специальных тайлов будет создаваться.|

<img src="https://redleggames.com/Games/TripletsAnimalMatch/Screens/GameplaySettings.png"/>

## 🧪 Технологии

| Система         | Использование                         |
|-----------------|---------------------------------------|
| `DoTween`       | Анимации                              |
| `Zenject`       | Зависимости между классами и сигналы  |
| `FMOD`          | Звуки и музыка                        |
| `Паттерны`      | Bootstrap, EntryPoint, MVP            |


## 🧾 Как запустить

Скачай архив, распакуй и запусти:

скачать с моего сайта -> 
https://redleggames.com/Games/TripletsAnimalMatch/PC_TripletsAnimalMatch.zip

скачать с Google Drive -> 
https://drive.google.com/file/d/1JWxUqeb3_fiC5slyh8CRiVeJ8NZ9yHAq/view?usp=sharing

Склонируй проект:
git clone https://github.com/DjKarp/Triplets_Animal_Match.git

Открыть в Unity 2022.3+ (URP)

Сцена запуска: Bootstrap

Играй! 🎉
