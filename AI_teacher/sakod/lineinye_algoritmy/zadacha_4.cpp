#include <iostream>
#include <vector>
#include <deque>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n, k;
    cin >> n >> k;

    vector<int> numbers(n);
    deque<int> window;

    for (int i = 0; i < n; i++) {
        cin >> numbers[i];

        while (!window.empty() && numbers[window.back()] >= numbers[i]) {
            window.pop_back();
        }

        window.push_back(i);

        if (window.front() <= i - k) {
            window.pop_front();
        }

        if (i >= k - 1) {
            cout << numbers[window.front()];
            if (i + 1 < n) {
                cout << '\n';
            }
        }
    }

    return 0;
}

/*
Пример входных данных:
5 2
1 -3 2 4 -5

Пример выходных данных:
-3
-3
2
-5

Сложность:
по времени: O(n), каждый индекс добавляется и удаляется из дека не больше одного раза;
по памяти: O(n), для массива чисел, сам дек хранит не больше k индексов.
*/
