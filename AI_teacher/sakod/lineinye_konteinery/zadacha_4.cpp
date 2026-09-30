#include <iostream>
#include <list>

using namespace std;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    list<int> numbers;
    for (int i = 0; i < n; i++) {
        int x;
        cin >> x;
        numbers.push_back(x);
    }

    int m;
    cin >> m;

    list<int>::iterator it = numbers.begin();
    int currentPosition = 1;

    for (int i = 0; i < m; i++) {
        int type, position;
        cin >> type >> position;

        while (currentPosition < position && it != numbers.end()) {
            ++it;
            currentPosition++;
        }

        if (type == 1) {
            int value;
            cin >> value;

            it = numbers.insert(it, value);
        } else {
            if (it != numbers.end()) {
                it = numbers.erase(it);
            }
        }
    }

    for (list<int>::iterator out = numbers.begin(); out != numbers.end(); ++out) {
        cout << *out << ' ';
    }

    return 0;
}

/*
Пример входных данных:
5
1 2 3 4 5
3
1 3 10
0 5
1 6 20

Пример выходных данных:
1 2 10 3 5 20

Сложность:
по времени: O(n + m), итератор по списку движется только вперед;
по памяти: O(n + m), если все изменения являются добавлениями.
*/
