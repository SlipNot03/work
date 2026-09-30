from distance import EuclideanDistance
import numpy as np


class Quality:
    def __init__(self):
        self.distance = EuclideanDistance()

    def calculate(self, X, expectations, centres):
        self._validate_calculate(X, expectations, centres)
        # МСумма квадратов расстояний до центров своих кластеров.
        general_error = 0.0
        for i in range(len(X)):
            cluster_number = expectations[i]
            cluster_centre = centres[cluster_number]
            general_error += (self.distance.calculate(X[i], cluster_centre))**2
        return general_error





    def _validate_calculate(self, X, expectations, centres):
        if not isinstance(X, np.ndarray) or not isinstance(expectations, np.ndarray) or not isinstance(centres, np.ndarray):
            raise TypeError("X, expectations и centres должны быть numpy массивами")
        
        if X.ndim != 2 or expectations.ndim != 1 or centres.ndim != 2:
            raise ValueError("нарушена размерность массивов")
        
        if not np.issubdtype(X.dtype, np.number) or not np.issubdtype(centres.dtype, np.number):
            raise TypeError("X и centres должны содержать числа")
        
        if len(centres) == 0:
            raise ValueError("centres должен содержать хотя бы один центр")
        
        if X.shape[1] != centres.shape[1]:
            raise ValueError("количество признаков в X и centres должно совпадать")

        if len(expectations) != len(X):
            raise ValueError("длина expectations должна совпадать с количеством объектов в X")

        if not np.issubdtype(expectations.dtype, np.integer):
                    raise TypeError("expectations должен содержать целые числа")
        
        if np.any(expectations < 0) or np.any(expectations >= len(centres)):
            raise ValueError("неверные значения в expectations")

        if len(X) == 0:
                    raise ValueError("X должен содержать хотя бы один объект")