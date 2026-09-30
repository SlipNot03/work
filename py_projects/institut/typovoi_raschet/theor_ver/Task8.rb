require_relative "Function/statistics_helper"


# Задание 8. Для заданного интервального выборочного ряда проверить гипотезу:
# закон распределения генеральной совокупности является нормальным.
# Вариант 1: alpha = 0.05, min_x = 10.1, h = 2.3, данные выборки занесены ниже.

#Я проверяю, подходит ли распределение. Сравниваю наблюдаемые и ожидаемые частоты через хи-квадрат.

# data - данные из варианта для проверки нормального распределения.
DATA_TASK8 = [2, 5, 20, 53, 78, 87, 89, 42, 13, 2]

def main
  # min_x - начало первого интервала.
  min_x = 10.1
  # h - шаг, то есть ширина интервала.
  h = 2.3
  # alpha - уровень значимости из условия.
  alpha = 0.05
  # n_intervals - сколько интервалов получилось.
  n_intervals = ((DATA_TASK8.max - min_x) / h).to_i + 1
  # bins - границы интервалов.
  bins = StatisticsHelper.linspace(min_x, min_x + n_intervals * h, n_intervals + 1)
  # observed_frequencies - реальные частоты по интервалам.
  observed_frequencies = StatisticsHelper.histogram(DATA_TASK8, bins)
  # mean - среднее; std_dev - стандартное отклонение.
  mean = StatisticsHelper.mean(DATA_TASK8)
  std_dev = StatisticsHelper.std(DATA_TASK8, sample: false)
  # expected_frequencies - частоты, которые должны быть при нормальном законе.
  expected_frequencies = []
  n_intervals.times do |i|
    # lower_bound и upper_bound - нижняя и верхняя границы интервала.
    lower_bound = bins[i]
    upper_bound = bins[i + 1]
    # prob - вероятность попасть в этот интервал.
    prob = StatisticsHelper.normal_cdf(upper_bound, mean, std_dev) -
           StatisticsHelper.normal_cdf(lower_bound, mean, std_dev)
    expected_frequencies << prob * DATA_TASK8.length
  end
  chi2_statistic = observed_frequencies.zip(expected_frequencies).sum do |real, exp|
    exp.zero? ? 0 : (real - exp) ** 2 / exp
  end
  chi2_crit = StatisticsHelper.chi_critical(DATA_TASK8.length - 2, alpha)
  puts "Статистика хи-квадрат наблюдаемое и критическое: #{chi2_statistic} #{chi2_crit}"
  if chi2_crit < chi2_statistic
    puts "Отказываем в нулевой гипотезе о нормальности распределения."
  else
    puts "Нет оснований для отказа в нулевой гипотезе о нормальности распределения."
  end
end

main if __FILE__ == $PROGRAM_NAME
