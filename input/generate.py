#!/usr/bin/env python
# coding=utf-8

import random
import sys
import argparse

parser = argparse.ArgumentParser()
parser.add_argument('-s', '--num-stops', type=int, default=20)
parser.add_argument('-t', '--num-trains', type=int, default=20)
parser.add_argument('-c', '--train-capacity', type=int, default=50)
ARGS = parser.parse_args(sys.argv[1:])

trains = []

for i in range(ARGS.num_trains):
    path_len = random.randint(2, ARGS.num_stops)
    path = random.sample(range(ARGS.num_stops), path_len)
    trains.append(path)

stops = [0] * ARGS.num_stops
for train in trains:
    for stop in train:
        stops[stop] += ARGS.train_capacity

print(','.join('%d:%d' % x for x in enumerate(stops)))
print(ARGS.num_trains)
print('\n'.join('%d:%s' % (i, ','.join(str(x) for x in path)) for i, path in enumerate(trains)))
print('')
print('numberOfBuses=%d' % ARGS.num_trains)
print('busCapacity=%d' % ARGS.train_capacity)
