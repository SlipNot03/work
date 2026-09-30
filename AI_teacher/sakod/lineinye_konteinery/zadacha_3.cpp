#include <iostream>
#include <deque>
#include <vector>
#include <algorithm>

using namespace std;

struct Visitor {
    int number;
    long long comeTime;
    long long workTime;
    int flag;
};

bool compareVisitors(const Visitor& first, const Visitor& second) {
    if (first.comeTime != second.comeTime) {
        return first.comeTime < second.comeTime;
    }

    return first.number < second.number;
}

void serveBefore(deque<Visitor>& people, vector<long long>& answer,
                 long long& freeTime, long long limitTime) {
    while (!people.empty()) {
        Visitor current = people.front();

        long long startTime = freeTime;
        if (startTime < current.comeTime) {
            startTime = current.comeTime;
        }

        if (startTime >= limitTime) {
            break;
        }

        freeTime = startTime + current.workTime;
        answer[current.number] = freeTime;
        people.pop_front();
    }
}

void serveUntil(deque<Visitor>& people, vector<long long>& answer,
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
        people.pop_front();
    }
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int n;
    cin >> n;

    if (n <= 0) {
        return 0;
    }

    deque<Visitor> people;
    vector<Visitor> visitors(n);
    vector<long long> answer(n);

    for (int i = 0; i < n; i++) {
        cin >> visitors[i].comeTime >> visitors[i].workTime >> visitors[i].flag;
        visitors[i].number = i;
    }

    bool sortedByTime = true;
    for (int i = 1; i < n; i++) {
        if (visitors[i].comeTime < visitors[i - 1].comeTime) {
            sortedByTime = false;
            break;
        }
    }

    if (!sortedByTime) {
        sort(visitors.begin(), visitors.end(), compareVisitors);
    }

    long long freeTime = 0;
    int index = 0;

    while (index < n) {
        long long currentTime = visitors[index].comeTime;

        serveBefore(people, answer, freeTime, currentTime);

        while (index < n && visitors[index].comeTime == currentTime) {
            if (visitors[index].flag == 1) {
                people.push_front(visitors[index]);
            } else {
                people.push_back(visitors[index]);
            }

            index++;
        }

        serveUntil(people, answer, freeTime, currentTime);
    }

    serveUntil(people, answer, freeTime, 4000000000000000000LL);

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
1 5 0
2 3 0
2 2 1
8 1 0

Пример выходных данных:
6
11
8
12

Сложность:
по времени: O(n), если данные уже идут по времени прихода, иначе O(n log n) из-за сортировки;
по памяти: O(n), для ответов, списка посетителей и дека ожидающих.
*/
