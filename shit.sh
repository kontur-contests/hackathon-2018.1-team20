#!/bin/bash
git co shitfiles

for (( i=1; i <= 100000; i++ ))
do
git ci -m "shitcommit #$i" --allow-empty
done