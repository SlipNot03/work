
# Тут хранится таблица критических значений для коэффициента Спирмена, чтобы проверять значимость ранговой связи.

module SpirmeTable
  module_function

  CRITICAL_VALUES = {
    5 => [0.94, nil], 6 => [0.85, nil], 7 => [0.78, 0.94], 8 => [0.72, 0.88],
    9 => [0.68, 0.83], 10 => [0.64, 0.79], 17 => [0.48, 0.62]
  }.freeze

  def get_data(alpha, n)
    values = CRITICAL_VALUES[n]
    return nil unless values

    alpha == 0.05 ? values[0] : values[1]
  end
end
