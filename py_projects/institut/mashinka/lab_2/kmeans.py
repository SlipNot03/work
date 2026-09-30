from expectation import Expectation
from maximization import Maximization
from quality import Quality
from initialization import InitializationCentre
import numpy as np


class KMeans:
    def __init__(self, k, n_init=10, max_iter=200):
        self._validate_k_n_init_max_iter(k, n_init, max_iter)
        self.k = k
        self.n_init = n_init
        self.max_iter = max_iter
        self.expectation = Expectation()
        self.maximization = Maximization()
        self.quality = Quality()

    def _run_once(self, X):
        initialization_centre = InitializationCentre(X, self.k)
        previous_expectations = None
        centres = initialization_centre.initialize()
        for i in range(self.max_iter):
            expectation = self.expectation.calculate(X, centres)
            #Стоп когда объекты перестали менять кластеры.
            if previous_expectations is not None and np.array_equal(expectation, previous_expectations):
                break
            new_centres = self.maximization.calculate(X, expectation, centres)
            centres = new_centres
            previous_expectations = expectation
        #После крайнего пересчёта центров обновляем итоговые метки
        expectation = self.expectation.calculate(X, centres)
        quality_rezult = self.quality.calculate(X, expectation, centres)
        return expectation, centres, quality_rezult

    def fit(self, X):
        self._validate_X(X)
        best_quality = float('inf')
        best_expectation = None
        best_centres = None
        for i in range(self.n_init):
            expectation, centres, quality_rezult = self._run_once(X)
            #Из нескольких запусков оставляем тот вариант,у которого минимальная внутрикластерной сумма
            if quality_rezult < best_quality:
                best_quality = quality_rezult
                best_expectation = expectation
                best_centres = centres
        self.centres = best_centres
        self.expectations = best_expectation
        self.quality_rezult = best_quality

        return  self.expectations, self.centres, self.quality_rezult





    
    def _validate_k_n_init_max_iter(self, k, n_init, max_iter):
        if not isinstance(k, int):
            raise TypeError("k должен быть целым числом")
        
        if k <= 0:
            raise ValueError("k должен быть положительным числом")

        if not isinstance(n_init, int):
            raise TypeError("n_init должен быть целым числом")

        if n_init <= 0:
            raise ValueError("n_init должен быть положительным числом")

        if not isinstance(max_iter, int):
            raise TypeError("max_iter должен быть целым числом")

        if max_iter <= 0:
            raise ValueError("max_iter должен быть положительным числом")

    def  _validate_X(self, X):
        if not isinstance(X, np.ndarray):
            raise TypeError("X должен быть numpy массивом")
        
        if X.ndim != 2:
            raise ValueError("нарушена двумерность X")

        if not np.issubdtype(X.dtype, np.number):
            raise TypeError("X должен содержать числа")
        
        if len(X) == 0:
            raise ValueError("X должен содержать хотя бы один объект")
        
        if X.shape[1] == 0:
            raise ValueError("X должен содержать хотя бы один признак")
        
        if len(X) < self.k:
            raise ValueError("количество объектов в X должно быть больше или равно k")
        
        if not np.isfinite(X).all():
            raise ValueError("X должен содержать только конечные числа")

        if self.k > len(np.unique(X, axis=0)):
            raise ValueError("k не может быть больше количества уникальных объектов в X")