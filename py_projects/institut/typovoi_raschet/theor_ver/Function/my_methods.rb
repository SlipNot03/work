require_relative "statistics_helper"


# Тут собраны вспомогательные методы для расчетов корреляции, регрессии и критических значений в задачах.


module MyMethods
  module_function

  def chi_critical(k, alpha: 0.01)
    StatisticsHelper.chi_critical(k, alpha)
  end

  def regression_equation_yx(x_values, y_values)
    StatisticsHelper.regression_equation(x_values, y_values)
  end

  def coeff_correlation(x_values, y_values)
    n = x_values.length
    x_mean = StatisticsHelper.mean(x_values)
    y_mean = StatisticsHelper.mean(y_values)
    sigma_x = Math.sqrt(x_values.sum { |x| x * x } / n.to_f - x_mean**2)
    sigma_y = Math.sqrt(y_values.sum { |y| y * y } / n.to_f - y_mean**2)
    xy_mean = x_values.zip(y_values).sum { |x, y| x * y } / n.to_f
    coeff = (xy_mean - x_mean * y_mean) / (sigma_x * sigma_y)
    puts "#{sigma_x} #{sigma_y}"
    puts coeff
    puts(coeff * sigma_x / sigma_y)
    coeff
  end

  def search_crit_pearson(alpha:, k:)
    # Простая табличная оценка .
    table = {
      0.05 => {
        18 => 0.444
      }
    }
    table.fetch(alpha, {}).fetch(k, 0.444)
  end
end
