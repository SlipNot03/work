import pandas as pd
import statsmodels.api as sm

# Загрузка данных
file_path = './dataset/2015.csv'
data = pd.read_csv(file_path)

# Определение зависимой и независимых переменных
X = data[['Economy (GDP per Capita)', 'Family', 'Health (Life Expectancy)', 'Freedom', 'Trust (Government Corruption)', 'Generosity', 'Dystopia Residual']]
y = data['Happiness Score']

# Добавление константы для модели
X = sm.add_constant(X)

# Построение модели
model = sm.OLS(y, X).fit()

# Вывод результатов
print(model.summary())