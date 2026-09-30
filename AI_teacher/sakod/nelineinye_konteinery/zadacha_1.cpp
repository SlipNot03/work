#include <iostream>
#include <set>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    set<int> numbers;

    for (int i = 0; i < n; i++) {
        int x;
        cin >> x;
        numbers.insert(x);
    }

    cout << numbers.size() << '\n';

    for (set<int>::iterator it = numbers.begin(); it != numbers.end(); ++it) {
        cout << *it << ' ';
    }

    return 0;
}

/*
Пример входных данных:
7
5 1 5 -2 3 1 0

Пример выходных данных:
5
-2 0 1 3 5

Сложность:
по времени: O(n log n), потому что вставка в set занимает O(log n);
по памяти: O(k), где k - количество различных чисел.
*/
