# Скриншотер

Скриншоты, запись экрана и редактор изображений для Windows.

[![Последний релиз](https://img.shields.io/github/v/release/w11hxhx/skrinshoter?label=версия)](https://github.com/w11hxhx/skrinshoter/releases/latest)
[![Сборка](https://github.com/w11hxhx/skrinshoter/actions/workflows/build.yml/badge.svg)](https://github.com/w11hxhx/skrinshoter/actions/workflows/build.yml)
![Windows 10 / 11](https://img.shields.io/badge/Windows-10%20%2F%2011-0078D4)
![Архитектура x64](https://img.shields.io/badge/архитектура-x64-555)

**[Скачать установщик](https://github.com/w11hxhx/skrinshoter/releases/latest/download/Skrinshoter-setup-win-x64.exe)** · **[Скачать portable](https://github.com/w11hxhx/skrinshoter/releases/latest/download/Skrinshoter-portable-win-x64.zip)** · [Что нового](https://github.com/w11hxhx/skrinshoter/releases/latest) · [Поддержка в Telegram](https://t.me/wiihxhx)

## Возможности

- **Захват экрана.** Скриншоты области, окна, монитора или всего рабочего стола.
- **Редактор.** Стрелки, линии, фигуры, текст, нумерация и размытие. Ненужные кнопки можно скрыть в настройках.
- **Запись экрана.** Запись видео с управлением горячими клавишами.
- **Сохранение и отправка.** Файл на диске, буфер обмена, печать или загрузка сразу в несколько сервисов.
- **Распознавание текста.** Локальное распознавание средствами Windows с копированием результата.
- **Поиск по фото.** Яндекс Картинки, Google Lens, Bing Visual Search и TinEye с открытием результатов в браузере.
- **Настраиваемые ссылки.** Копирование с названиями сервисов или только самих ссылок.

Сервисы загрузки: **Скриншотер · kappa.lol · Xyli · 0xl1r.cfd · NoikCloud**. Выберите нужные в настройках. Для поиска по фото снимок предварительно загружается на kappa.lol.

## Установка

Требуются **Windows 10/11 x64** и **.NET Framework 4.8**.

| Вариант | Как запустить |
| --- | --- |
| [Установщик](https://github.com/w11hxhx/skrinshoter/releases/latest/download/Skrinshoter-setup-win-x64.exe) | Запустите EXE и следуйте шагам установки. |
| [Portable](https://github.com/w11hxhx/skrinshoter/releases/latest/download/Skrinshoter-portable-win-x64.zip) | Распакуйте ZIP в отдельную папку и запустите `Skrinshoter.exe`. |

В [каждом релизе](https://github.com/w11hxhx/skrinshoter/releases) также доступны исходники и файл `SHA256SUMS.txt` с контрольными суммами.

## Горячие клавиши

Комбинации по умолчанию можно изменить в настройках.

| Клавиши | Действие |
| --- | --- |
| `Print Screen` | Снимок области |
| `Alt + Print Screen` | Снимок активного окна |
| `Shift + Print Screen` | Снимок активного монитора |
| `Ctrl + Print Screen` | Снимок всего рабочего стола |
| `Ctrl + Shift + Print Screen` | Запись экрана |
| `Shift + Pause` | Пауза записи |

В редакторе:

| Клавиши | Действие |
| --- | --- |
| `Enter` | Загрузить в Скриншотер |
| `Ctrl + Enter` | Загрузить во все выбранные сервисы |
| `Ctrl + C` | Скопировать изображение |
| `Ctrl + S` | Сохранить файл |
| `Ctrl + P` | Распечатать |
| `Esc` | Закрыть редактор |

Для распознавания текста используются языки распознавания, установленные в Windows. Выбор поисковиков, кнопок редактора и формата ссылок находится в настройках скриншотов.

## Обратная связь

О проблемах и предложениях можно написать в [Issues](https://github.com/w11hxhx/skrinshoter/issues) или [Telegram](https://t.me/wiihxhx). Укажите версию приложения, версию Windows и шаги для повторения проблемы.

## Разработчикам

Откройте PowerShell в корне репозитория:

```powershell
# Сборка приложения и portable-архива
.\build.ps1

# Проверка настроек
.\tests\Verify-ScreenshotSettings.ps1

# Полный релиз: portable, установщик, исходники и контрольные суммы
.\release.ps1
```

Сценарии устанавливают нужный .NET SDK в `.dotnet`, а Inno Setup — в `.tools`. Эти каталоги исключены из Git.

GitHub Actions собирает проект при push и pull request в `main`. Теги вида `v4.87.4.2` создают релиз с готовыми файлами и описанием из [RELEASE_NOTES.md](RELEASE_NOTES.md).

Исходный код реконструирован из .NET-сборок. Лицензия FFmpeg поставляется рядом с модулем записи в готовых сборках.
