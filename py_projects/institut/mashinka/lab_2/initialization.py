import numpy as np


class InitializationCentre:
    def __init__(self, X, k):
        self._validate_X(X)
        self.X = X
        self._validate_k(k)
        self.k = k

    def initialize(self):
        #Убираем дубликаты чтобы центры не совпали
        X_unique = np.unique(self.X, axis=0)
        samples, no_use = X_unique.shape
        random_ind = np.random.choice(samples, size=self.k, replace=False)
        centres = X_unique[random_ind]
        return centres






    def _validate_X(self, X):
        if not isinstance(X, np.ndarray):
            raise TypeError("X должен быть numpy массивом")
        
        if X.ndim != 2:
            raise ValueError("нарушена двумерность X")

        if not np.issubdtype(X.dtype, np.number):
            raise TypeError("X должен содержать числа")
        
    def _validate_k(self, k):
        if not isinstance(k, int):
            raise TypeError("k должен быть целым числом")
        
        if k <= 0:
            raise ValueError("k должен быть положительным числом")
        
        if k > len(np.unique(self.X, axis=0)):
            raise ValueError("k не может быть больше количества уникальных объектов в X")