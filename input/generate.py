#!/usr/bin/env python
# coding=utf-8

import random

NUM_STOPS = 20
NUM_TRAINS = 20
TRAIN_CAPACITY = 50

trains = []

for i in range(NUM_TRAINS):
    path_len = random.randint(2, NUM_STOPS)
    path = random.sample(range(NUM_STOPS), path_len)
    trains.append(path)

stops = [0] * NUM_STOPS
for train in trains:
    for stop in train:
        stops[stop] += TRAIN_CAPACITY

print(','.join('%d:%d' % x for x in enumerate(stops)))
print(NUM_TRAINS)
print('\n'.join('%d:%s' % (i, ','.join(str(x) for x in path)) for i, path in enumerate(trains)))
print('')
print('numberOfBuses=%d' % NUM_TRAINS)
print('busCapacity=%d' % TRAIN_CAPACITY)
