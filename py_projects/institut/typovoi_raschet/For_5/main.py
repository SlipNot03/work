import numpy as np                  # для работы с матрицами, линейной алгеброй
import pandas as pd                 # для загрузки, очистки и обработки данных
import matplotlib.pyplot as plt     # для визуализации
import seaborn as sns               # для построения heatmap (тепловой карты)
from sklearn.linear_model import LinearRegression        # для реализации линейной регрессии
from sklearn.model_selection import train_test_split     # для разделения данных на обучающую и тестовую выборки
from sklearn.metrics import mean_squared_error           # для расчета метрики ошибки
from sklearn.preprocessing import StandardScaler         # для стандартизации данных (нормализация)


#! Суть исследования — спрогнозировать оценку уровня счастья (Score) на основе двух факторов:
#! ВВП на душу населения (GDP per capita).
#! Ожидаемая продолжительность здоровой жизни (Healthy life expectancy).
# https://www.kaggle.com/datasets/unsdsn/world-happiness?resource=download&select=2019.csv
#? pip install scikit-learn numpy matplotlib seaborn

# Загрузка данных о уровне счастья в мире
file_path = './dataset/2015.csv'
data = pd.read_csv(file_path)

# Предварительный просмотр данных
print("\nПервые строки данных:")
print(data.head())
data_clean = data.dropna()

# Отбор только числовых столбцов
numeric_data = data_clean.select_dtypes(include=[np.number])

# Расчет корреляционной матрицы
correlation_matrix = numeric_data.corr()

# Построение heatmap
plt.figure(figsize=(10, 8))
sns.heatmap(correlation_matrix, annot=True, cmap='coolwarm', fmt=".2f")
plt.title('Корреляционная матрица числовых признаков')
plt.show()

# Признаки и целевая переменная
X = data_clean[['Economy (GDP per Capita)', 'Health (Life Expectancy)']]   # входные признаки - ввп на душу, ожидаемая продолжительность жизни
y = data_clean['Happiness Score']                                         # целевая переменная

# Нормализация данных(приведение к нулевому среднему и единичному стандартному отклонению).
scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# Разделение данных на обучающую и тестовую выборки
X_train, X_test, y_train, y_test = train_test_split(X_scaled, y, test_size = 0.2, random_state = 42)
# Данные разделяются на обучающую (80%) и тестовую (20%) выборки.
# random_state = 42 — для воспроизводимости результатов

# Линейная регрессия (sklearn) (метод наименьших квадратов)
model = LinearRegression()
model.fit(X_train, y_train)   # обучение модели на тренировочных данных

# Коэффициенты модели (β1, β2, ... для признаков)
coefficients = model.coef_

# Свободный член (β0)
intercept = model.intercept_

print(f"Коэффициенты β (для признаков): {coefficients}")   # весовой коэффициент признаков
print(f"Свободный член β0: {intercept}")                   # значение целевой переменной (смещение верх и вниз)

# Предсказания
y_pred = model.predict(X_test)

# Коэффициенты и свободный член
beta = model.coef_
beta_0 = model.intercept_

# Первая строка нормализованных данных
first_row = X_scaled[67]

# Прогнозируемое значение для первой строки
y_pred_first = beta_0 + np.dot(beta, first_row)

print(f"Прогнозируемое значение для первой строки: {y_pred_first}")

# Вычисление ошибки
mse = mean_squared_error(y_test, y_pred)            # среднеквадратичная ошибка (отражает степень расхождения предсказаний и реальных значений)
print(f'\nСреднеквадратичная ошибка (Sklearn): {mse}')

# Построение графика (реальные значения против прогнозируемых)
plt.scatter(y_test, y_pred)
plt.plot([min(y_test), max(y_test)], [min(y_test), max(y_test)], color='red', linestyle='--')
plt.xlabel('реальные значения')
plt.ylabel('прогнозируемые значения')
plt.title('Реальное против прогнозируемое (Sklearn Linear Regression)')
plt.show()


# Добавление столбца единиц для учета свободного члена
X_train_bias = np.c_[np.ones(X_train.shape[0]), X_train]
X_test_bias = np.c_[np.ones(X_test.shape[0]), X_test]

# Инициализация параметров
theta_initial = np.zeros(X_train_bias.shape[1])

# Градиентный спуск
def gradient_descent(X, y, theta, learning_rate, iterations):
    m = len(y)
    cost_history = []

    for i in range(iterations):
        prediction = X.dot(theta)
        error = prediction - y
        theta = theta - (learning_rate / m) * X.T.dot(error)
        cost = (1 / (2 * m)) * np.sum(error ** 2)
        cost_history.append(cost)

    return theta, cost_history

# Параметры градиентного спуска
learning_rate = 0.01
iterations = 1000

# Выполнение градиентного спуска
theta_optimal, cost_history = gradient_descent(X_train_bias, y_train, theta_initial, learning_rate, iterations)

# Предсказания и ошибка
y_pred_gd = X_test_bias.dot(theta_optimal)
mse_gd = mean_squared_error(y_test, y_pred_gd)
print(f'Среднеквадратичная ошибка (Gradient Descent): {mse_gd}')

# Вывод первых и последних значений изменения ошибки
print("\nПервые 5 значений ошибки:")
for value in cost_history[:5]:
    print(f"{value:.6f}")

print("\nПоследние 5 значений ошибки:")
for value in cost_history[-5:]:
    print(f"{value:.6f}")

# График минимизации ошибки
plt.plot(range(iterations), cost_history)
plt.xlabel('Итерация')
plt.ylabel('Ошибка (Cost)')
plt.title('Минимизация ошибки в градиентном спуске')
plt.show()

# График предсказанных значений
plt.scatter(y_test, y_pred_gd)
plt.plot([min(y_test), max(y_test)], [min(y_test), max(y_test)], color='red', linestyle='--')
plt.xlabel('реальные значения')
plt.ylabel('прогнозируемые значения')
plt.title('Реальное против предсказанного (Gradient Descent)')
plt.show()

