# Headshot Explode

Плагин создаёт эффект взрыва головы, если убил игрока в голову.

# Задачи

- [x] Партикль при убийстве игрока в голову
- [x] Звук при убийстве игрока в голову

# Квары

| Название | Значение | Описание |
|--|--|--|
| hexd_enable | true/false | Включить плагин? |
| hexd_particle_file | "particles/explosions_fx/c4_train_ground_effect.vpcf" **(По умолчанию)**  | Файл партикля. |
| hexd_particle_life_time | 1 **(По умолчанию)** | Время жизни партикля. |
| hexd_particle_origin | "0 0 50" **(По умолчанию)** | Смещение позиции партикля относительно ног игрока. |
| hexd_sound | "hexd.explode" **(По умолчанию)** | Звук взрыва. |

# Установка


1. Скачать [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp/releases/tag/v1.0.318)
2. Скачать [Metamod](https://www.sourcemm.net/downloads.php?branch=dev)
3. Скачать [релиз](https://github.com/XummukProgrammer/HeadshotExplode/releases/tag/v1.0)
4. Распаковать архив из релиза на сервер (Без папки soundevents, её нужно поставить в другое место)
5. Скачать [ResourcePrecasher](https://github.com/KillStr3aK/ResourcePrecacher/releases/tag/1.0.7s)
6. В ResourcePrecasher занести новые ресурсы: партикль (из **hexd_particle_file**), а также звуковой ивент, в котором находится звук (из **hexd_sound**)
7. Создаём свой аддон и заливаем в него содержимое soundevents
8. Скачать [MultiAddonManager](https://github.com/Source2ZE/MultiAddonManager/releases/tag/v1.4)
9. В MultiAddonManager прописать идентификатор созданного аддона
10. Запускаем сервер и проверяем