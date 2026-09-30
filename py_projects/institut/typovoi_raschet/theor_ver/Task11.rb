require_relative "Function/statistics_helper"


# Задание 11. По данным выборки проверить с помощью критерия Пирсона гипотезы:
# а) о показательном законе распределения;
# б) о равномерном законе распределения;
# в) о нормальном законе распределения генеральной совокупности.
# В ответе привести выбранную гипотезу, вычисленное значение критерия, критическое значение и вывод.
# Вариант 1: alpha = 0.010, данные выборки занесены ниже.

#Я проверяю, подходит ли распределение. Сравниваю наблюдаемые и ожидаемые частоты через хи-квадрат.

# data - данные из варианта, их проверяю на три распределения.
DATA_TASK11 = [
  16.9, 7.6, -8.4, 4.2, 14.3, 12.4, 6.7, 17.5, 1.1, 15.4,
  15.1, 4.2, -0.4, 13.1, 13.5, 16.0, 27.3, 11.7, 11.7, 8.7,
  11.9, 18.0, -8.0, 16.0, 14.7, 10.8, 2.5, 0.4, 8.0, 10.8,
  16.3, 12.9, 12.0, 9.6, 9.9, 6.7, 3.4, 11.0, 20.4, 18.4,
  6.1, 16.3, 3.8, 15.7, 22.9, 7.9, 20.5, 12.5, 11.8, 15.9,
  5.6, 16.3, 15.7, 7.2, 15.8, 9.5, 13.3, 5.7, 15.5, 18.3,
  15.0, 9.0, 6.0, 7.4, 11.0, 24.0, 9.9, 8.2, 12.9, 4.3,
  20.5, 2.5, 10.5, 12.4, 8.7, 12.1, 3.1, -1.5, 8.6, 14.9,
  16.6, 19.5, 13.9, 13.8, 14.7, -0.7, 16.4, 6.7, 16.5, 11.6,
  13.2, 15.2, 17.4, 13.3, 11.4, 10.0, 9.6, 16.5, 20.5, 7.5
]
# alpha - уровень значимости из условия.
ALPHA_TASK11 = 0.010

def print_result(name, chi_squared, critical_value)
  # name - какое распределение проверяю.
  # chi_squared - что получилось по хи-квадрат; critical_value - табличная граница.
  puts format("%s: χ² = %.4f, критическое значение = %.4f", name, chi_squared, critical_value)
  puts(chi_squared > critical_value ? "гипотеза отклоняется" : "нет оснований отвергать гипотезу")
end

def test_exponential(sample, alpha = ALPHA_TASK11)
  # shifted - сдвигаю данные, чтобы не было отрицательных значений.
  shifted = sample.map { |x| x - sample.min }
  lambda_param = 1.0 / StatisticsHelper.mean(shifted)
  bins = StatisticsHelper.linspace(shifted.min, shifted.max, 8)
  observed = StatisticsHelper.histogram(shifted, bins)
  expected = (0...(bins.length - 1)).map do |i|
    shifted.length * (Math.exp(-lambda_param * bins[i]) - Math.exp(-lambda_param * bins[i + 1]))
  end
  chi_squared = observed.zip(expected).sum { |real, exp| exp.zero? ? 0 : (real - exp) ** 2 / exp }
  critical_value = StatisticsHelper.chi_square_ppf(1 - alpha, observed.length - 2)
  print_result("Показательное распределение", chi_squared, critical_value)
end

def test_uniform(sample, alpha = ALPHA_TASK11)
  bins = StatisticsHelper.linspace(sample.min, sample.max, 6)
  observed = StatisticsHelper.histogram(sample, bins)
  expected = Array.new(observed.length, sample.length.to_f / observed.length)
  chi_squared = observed.zip(expected).sum { |real, exp| (real - exp) ** 2 / exp }
  critical_value = StatisticsHelper.chi_square_ppf(1 - alpha, observed.length - 1)
  print_result("Равномерное распределение", chi_squared, critical_value)
end

def test_normal(sample, alpha = ALPHA_TASK11)
  mu = StatisticsHelper.mean(sample)
  sigma = StatisticsHelper.std(sample, sample: false)
  bins = StatisticsHelper.linspace(sample.min, sample.max, 8)
  observed = StatisticsHelper.histogram(sample, bins)
  expected = (0...(bins.length - 1)).map do |i|
    sample.length * (StatisticsHelper.normal_cdf(bins[i + 1], mu, sigma) -
      StatisticsHelper.normal_cdf(bins[i], mu, sigma))
  end
  chi_squared = observed.zip(expected).sum { |real, exp| exp.zero? ? 0 : (real - exp) ** 2 / exp }
  critical_value = StatisticsHelper.chi_square_ppf(1 - alpha, observed.length - 3)
  print_result("Нормальное распределение", chi_squared, critical_value)
end

def main(sample = DATA_TASK11, alpha = ALPHA_TASK11)
  # sample - выборка; alpha - уровень значимости.
  test_exponential(sample, alpha)
  test_uniform(sample, alpha)
  test_normal(sample, alpha)
end

main if __FILE__ == $PROGRAM_NAME
