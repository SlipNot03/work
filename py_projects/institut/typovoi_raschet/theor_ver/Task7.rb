require_relative "Function/statistics_helper"


# Задание 7. Проверить гипотезу с помощью критерия Пирсона.
# Вариант 1: при испытании радиоэлектронной аппаратуры фиксировалось число отказов.
# По 60 испытаниям проверить гипотезу о том, что число отказов имеет распределение Пуассона.
# Дано: число отказов 0, 1, 2, 3; число испытаний 42, 11, 4, 3; alpha = 0.05.

#Я проверяю, подходит ли распределение. Сравниваю наблюдаемые и ожидаемые частоты через хи-квадрат.
 
# failures - сколько отказов может быть.
FAILURES = [0, 1, 2, 3]
# observed - сколько раз встретилось каждое число отказов.
OBSERVED = [42, 11, 4, 3]
# alpha - уровень значимости из условия.
ALPHA_TASK7 = 0.05

def main(x = FAILURES, freq = OBSERVED, alpha = ALPHA_TASK7)
  # x - числа отказов, freq - их частоты.
  # n - всего наблюдений.
  n = freq.sum
  # mean - тут получается lambda для Пуассона.
  mean = x.zip(freq).sum { |value, count| value * count }.to_f / n
  # expected - какие частоты должны быть по Пуассону.
  expected = x.map { |value| n * StatisticsHelper.poisson_pmf(value, mean) }
  # chi2_observed - посчитанный хи-квадрат.
  chi2_observed = freq.zip(expected).sum { |real, exp| (real - exp) ** 2 / exp }
  # df - степени свободы для таблицы.
  df = x.length - 1 - 1
  # chi2_critical - критическое значение из таблицы.
  chi2_critical = StatisticsHelper.chi_square_ppf(1 - alpha, df)

  puts format("lambda = %.4f", mean)
  puts format("Наблюдаемое значение χ² = %.4f", chi2_observed)
  puts format("Критическое значение χ² = %.4f", chi2_critical)
  if chi2_observed < chi2_critical
    puts "Нет оснований отвергать гипотезу о распределении Пуассона."
  else
    puts "Гипотеза о распределении Пуассона отвергается."
  end
end

main if __FILE__ == $PROGRAM_NAME
