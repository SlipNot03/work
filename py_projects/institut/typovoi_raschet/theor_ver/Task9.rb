require_relative "Function/statistics_helper"


# Задание 9. Для заданного интервального выборочного ряда с начальным значением min_x = 0
# и шагом h проверить гипотезу: закон распределения генеральной совокупности является показательным.
# Вариант 1: alpha = 0.01, h = 1.0, данные выборки занесены ниже.

#Я проверяю, подходит ли распределение. Сравниваю наблюдаемые и ожидаемые частоты через хи-квадрат.

# alpha - уровень значимости из условия.
ALPHA_TASK9 = 0.01
# min_x - начало первого интервала.
MIN_X_TASK9 = 0
# h - шаг интервала.
H_TASK9 = 1.0
# data - частоты из интервального ряда.
DATA_TASK9 = [206, 111, 48, 26, 15, 3, 6, 0, 0, 0, 1]

def test_exponential(data, alpha = ALPHA_TASK9)
  # data - данные, которые проверяю на показательное распределение.
  # lambda_param - параметр lambda, считаю его по среднему.
  lambda_param = 1.0 / StatisticsHelper.mean(data)
  min_value = data.min
  max_value = data.max
  bins = StatisticsHelper.linspace(min_value, max_value, [2, Math.sqrt(data.length).ceil + 1].max)
  # observed_freq - реальные частоты по интервалам.
  observed_freq = StatisticsHelper.histogram(data, bins)
  # expected_freq - частоты, которые должны быть по показательному закону.
  expected_freq = (0...(bins.length - 1)).map do |i|
    data.length * (Math.exp(-lambda_param * bins[i]) - Math.exp(-lambda_param * bins[i + 1]))
  end
  # chi_squared - посчитанный хи-квадрат.
  chi_squared = observed_freq.zip(expected_freq).sum { |real, exp| exp.zero? ? 0 : (real - exp) ** 2 / exp }
  # df - степени свободы.
  df = observed_freq.length - 1
  # critical_value - критическое значение из таблицы.
  critical_value = StatisticsHelper.chi_square_ppf(1 - alpha, df)
  puts format("Показательное распределение: χ² = %.4f, критическое значение = %.4f", chi_squared, critical_value)
  puts(chi_squared > critical_value ? "гипотеза отклоняется" : "нет оснований отвергать гипотезу ")
  [chi_squared, critical_value, chi_squared > critical_value]
end

# x - точки интервалов, восстановленные по min_x и h.
x = Array.new(DATA_TASK9.length) { |i| MIN_X_TASK9 + H_TASK9 * i }
# my_data - данные после преобразования для проверки.
my_data = (0...(x.length - 1)).map { |i| DATA_TASK9[i] * (x[i + 1] - x[i]) }
puts my_data.inspect
test_exponential(my_data)
