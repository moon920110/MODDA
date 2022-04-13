import os
import numpy as np
import pandas as pd


root_path = './data'
sensor = 'GSR'

players = os.listdir(os.path.join(root_path, sensor))

cnt = 0
total_cnt = 0
p = {}
pp = {}
for player in players:
    games = os.listdir(os.path.join(root_path, sensor, player))
    for game in games:
        if 'for_gsr' in game:
            continue
        df = pd.read_csv(os.path.join(root_path, sensor, player, game))
        total_cnt += 1
        ### code below is searching non-recorded GSR timelines. You can ignore it
        if len(df) == 0:
            if 'KeyBoard_KeyBoard' not in game:
                if player not in p:
                    p[player] = 1
                else:
                    p[player] += 1
                # print(player, game)
            else:
                if player not in pp:
                    pp[player] = 1
                else:
                    pp[player] += 1
            continue
        ### Until here

        df.pop('TIME')
        df.pop('DATE')
        df['DATETIME'] = pd.to_datetime(df['DATETIME'])
        # print(player, game, df)
        df['DATETIME'] -= df['DATETIME'].iloc[0]
        df['DATETIME'] = df['DATETIME'].apply(pd.Timedelta.total_seconds)
        df.to_csv(os.path.join(root_path, sensor, player, f'for_gsr_{game}'), index=False)

print(total_cnt, p, len(p.keys()))
print(pp, len(pp.keys()))