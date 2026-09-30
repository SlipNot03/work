require_relative "Function/statistics_helper"


# Задание 10. Для заданного интервального выборочного ряда проверить гипотезу:
# закон распределения генеральной совокупности является равномерным.
# Вариант 1: alpha = 0.05, min_x = -102.9, h = 1.8, данные выборки занесены ниже.

#Я проверяю, подходит ли распределение. Сравниваю наблюдаемые и ожидаемые частоты через хи-квадрат.

# data_p - частоты из условия.
DATA_P_TASK10 = [44, 39, 49, 45, 52, 33, 42, 45, 48, 40]

def main(data = DATA_P_TASK10, min_x = -102.9, h = 1.8, alpha = 0.05)
  # data - данные, по которым проверяю равномерность.
  # min_x - начало первого интервала; h - шаг интервала.
  # alpha - уровень значимости из условия.
  n_intervals = ((data.max - min_x) / h).to_i + 1
  bins = StatisticsHelper.linspace(min_x, min_x + n_intervals * h, n_intervals + 1)
  observed_frequencies = StatisticsHelper.histogram(data, bins)
  expected = Array.new(observed_frequencies.length, data.length.to_f / observed_frequencies.length)
  chi2_statistic = observed_frequencies.zip(expected).sum { |real, exp| (real - exp) ** 2 / exp }
  chi2_crit = StatisticsHelper.chi_critical(data.length - 2, alpha)
  puts "Статистика хи-квадрат наблюдаемое и критическое: #{chi2_statistic} #{chi2_crit}"
  if chi2_crit < chi2_statistic
    puts "Отказываем в нулевой гипотезе о равномерности распределения."
  else
    puts "Нет оснований для отказа в нулевой гипотезе о равномерности распределения."
  end
end

main if __FILE__ == $PROGRAM_NAME
