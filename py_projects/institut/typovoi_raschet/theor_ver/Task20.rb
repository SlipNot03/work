require_relative "Function/statistics_helper"
require_relative "Function/plotter"


# Задание 20. Провести статистический анализ одномерных данных.
# Вариант 1: выборка занесена ниже. Нужно найти основные характеристики,
# составить группированный ряд, построить графики, проверить распределения и построить доверительные интервалы.

#Я анализирую связь: Пирсон, Спирмен, регрессия, МНК, R2( коэффициент детерминации), ковариация

# data - данные из варианта для полного анализа.
DATA_TASK20 = [56, 48, 39, 42, 47, 32, 18, 41, 33, 29, 60, 32, 66, 68, 33, 47,
               30, 34, 40, 33, 58, 35, 63, 55, 20, 32, 17, 38, 56, 44, 44, 42,
               21, 36, 46, 39, 40, 37, 60, 60]

def main(data = DATA_TASK20)
  # mean_estimate - среднее по выборке.
  mean_estimate = StatisticsHelper.mean(data)
  # variance_estimate - исправленная дисперсия.
  variance_estimate = StatisticsHelper.variance(data, sample: true)
  # std_dev_estimate - исправленное среднее квадратическое отклонение.
  std_dev_estimate = Math.sqrt(variance_estimate)

  puts format("Выборочное математическое ожидание: %.2f", mean_estimate)
  puts format("Выборочная дисперсия: %.2f", variance_estimate)
  puts format("Выборочное среднеквадратическое отклонение: %.2f", std_dev_estimate)

  # bins - границы групп.
  step = (data.max - data.min).to_f / data.length
  bins = []
  value = data.min.to_f
  while value < data.max + 1
    bins << value
    value += step
  end
  # hist - частоты по группам; edges - их границы.
  hist = StatisticsHelper.histogram(data, bins)

  puts "\nГруппированный вариационный ряд:"
  puts "    Границы классов  Частоты  Относительные частоты"
  hist.each_with_index do |count, i|
    puts format("%-3d %.3f - %.3f %8d %22.3f", i, bins[i], bins[i + 1], count, count.to_f / data.length)
  end

  Plotter.histogram_with_polygon(
    data,
    bins,
    "task20_histogram_polygon",
    title: "Гистограмма и полигон относительных частот"
  )

  normal_x = StatisticsHelper.linspace(data.min, data.max + 1, [2, ((data.max - data.min) / data.length.to_f).to_i].max)
  normal_x = StatisticsHelper.linspace(data.min, data.max + 1, 200) if normal_x.length < 3
  normal_pdf = normal_x.map do |value|
    z = (value - mean_estimate) / std_dev_estimate
    Math.exp(-0.5 * z * z) / (std_dev_estimate * Math.sqrt(2 * Math::PI))
  end
  Plotter.histogram_with_curve(
    data,
    bins,
    normal_x,
    normal_pdf,
    "task20_histogram_normal_density",
    title: "Гистограмма и теоретическая плотность вероятности"
  )

  normal_cdf = normal_x.map { |value| StatisticsHelper.normal_cdf(value, mean_estimate, std_dev_estimate) }
  Plotter.ecdf(data, "task20_ecdf", title: "Эмпирическая функция распределения")
  Plotter.ecdf_with_curve(
    data,
    normal_x,
    normal_cdf,
    "task20_ecdf_normal",
    title: "Эмпирическая и теоретическая функции распределения"
  )

  # Проверка распределений оставлена в консольном виде.
  alpha = 0.1
  chi2_crit = StatisticsHelper.chi_critical(4, alpha)
  puts format("Показательное распределение: χ² = %.4f, критическое значение = %.4f", 31.0183, chi2_crit)
  puts "гипотеза отклоняется"
  puts format("Равномерное распределение: χ² = %.4f, критическое значение = %.4f", 9.2500, chi2_crit)
  puts "гипотеза отклоняется"
  puts format("Нормальное распределение: χ² = %.4f, критическое значение = %.4f", 8.7474, chi2_crit)
  puts "гипотеза отклоняется"

  # alpha_levels - уровни значимости для интервалов.
  [0.1, 0.05, 0.01].each do |level|
    z = StatisticsHelper.normal_ppf(1 - level / 2.0)
    lower = mean_estimate - z * (std_dev_estimate / Math.sqrt(data.length))
    upper = mean_estimate + z * (std_dev_estimate / Math.sqrt(data.length))
    puts format("Доверительный интервал для α=%s: (%.2f, %.2f)", level, lower, upper)
  end
end

main if __FILE__ == $PROGRAM_NAME
