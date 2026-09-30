#include <iostream>
#include <queue>
#include <vector>

using namespace std;

struct Visitor {
    int number;
    long long comeTime;
    long long workTime;
};

void serveAvailable(queue<Visitor>& people, vector<long long>& answer,
                    long long& freeTime, long long limitTime) {
    while (!people.empty()) {
        Visitor current = people.front();

        long long startTime = freeTime;
        if (startTime < current.comeTime) {
            startTime = current.comeTime;
        }

        if (startTime > limitTime) {
            break;
        }

        freeTime = startTime + current.workTime;
        answer[current.number] = freeTime;
        people.pop();
    }
}

int main() {
    int n;
    cin >> n;

    if (n <= 0) {
        return 0;
    }

    queue<Visitor> people;
    vector<long long> answer(n);
    long long freeTime = 0;

    for (int i = 0; i < n; i++) {
        long long comeTime, workTime;
        cin >> comeTime >> workTime;

        serveAvailable(people, answer, freeTime, comeTime);

        Visitor visitor;
        visitor.number = i;
        visitor.comeTime = comeTime;
        visitor.workTime = workTime;
        people.push(visitor);

        serveAvailable(people, answer, freeTime, comeTime);
    }

    serveAvailable(people, answer, freeTime, 4000000000000000000LL);

    for (int i = 0; i < n; i++) {
        cout << answer[i];
        if (i + 1 < n) {
            cout << '\n';
        }
    }

    return 0;
}

/*
Пример входных данных:
4
1 5
2 3
6 2
12 4

Пример выходных данных:
6
9
11
16

Сложность:
по времени: O(n), каждый посетитель один раз попадает в очередь и один раз выходит из нее;
по памяти: O(n), для ответов и очереди ожидающих посетителей.
*/
