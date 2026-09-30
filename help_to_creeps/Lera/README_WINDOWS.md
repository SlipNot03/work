# Запуск проекта на Windows

1. Скопируйте всю папку проекта из Ubuntu в любую обычную папку Windows, например:

```text
C:\Users\<Ваш пользователь>\Documents\Lera
```

2. Установите Python 3 с сайта https://www.python.org/downloads/.

Во время установки обязательно включите:

```text
Add python.exe to PATH
```

3. Откройте скопированную папку проекта в Windows и запустите:

```text
setup_windows.bat
```

Этот файл создаст `.venv` и установит зависимости из `requirements.txt`.

4. После установки запускайте нужный расчет:

```text
run_heated.bat
run_unheated.bat
```

Или оба расчета подряд:

```text
run_all_windows.bat
```

5. Графики сохраняются в папки:

```text
graphs_heated
graphs_unheated
```

Окна графиков по умолчанию не открываются, чтобы Windows не зависал на десятках окон. PNG-файлы сохраняются автоматически.
