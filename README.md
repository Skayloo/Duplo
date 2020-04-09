# SecretService C# program

Программу необходимо запускать из-под имени администратора (по умолчанию он предлагает).
## Сборка

Для сброки необходим дополнительный софт часть можно установить при помощи Nuget
Install-Package Hardcodet.Wpf.TaskbarNotification
Install-Package RibbonControlsLibrary
Install-Package Extended.Wpf.Toolkit

Есть пакет (Microsoft.Windows.Shell -Version 3.0.1) через nuget ставится, но проект не собирается, лучше подключить через ссылку
Также понадобится пакет Microsoft.Deployment.WindowsInstaller. Получить его можно двумя способами:
1 - Скачать Wix.ToolSet и установить 2 гб себе на компьютер. И пярмо за ним установить предлагаемый VSIX.
2 - Использовать в прикрепленной директории Libraries - Microsoft.Deployment.WindowsInstaller.dll

## Доп

Для всего функционала который я реализовал, и чтобы программа не вылетала нам понадобится Nauz File Detector
Скачать можно по ссылке: https://github.com/horsicq/Nauz-File-Detector/releases
Скачиваем, из архива вытаскиваем папку под названием "base" и закидываем её целиком в папку с проектом