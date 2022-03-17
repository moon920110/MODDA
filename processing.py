import glob
import os
from datetime import datetime, time

import numpy as np
import pandas as pd


def test():
    root_path = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection'
    game_log_files = os.listdir(os.path.join(root_path, f'pre-collected_user_data_csv/p1/features'))
    df = pd.read_csv('data/TEMP/p1.csv', header=None)
    for file in game_log_files:
        if '1P' in file:
            game_time = file.split('_')[-1].split('-')[-1][:-9]
            start_time = time(int(game_time.split('.')[0]), int(game_time.split('.')[1]), int(game_time.split('.')[2]))
            log_df = pd.read_csv(os.path.join(root_path, f'pre-collected_user_data_csv/p1/features', file))
            print(log_df['elapsed_milli_time'])


def main():
    num = np.arange(1, 43, 1)
    sensor = 'TEMP'
    root_path = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection'

    if not os.path.exists(f'./{sensor}'):
        os.mkdir(f'./{sensor}')

    for number in range(1, num.shape[0]+1):
        dataframe = []
        participant = f"p{number}"
        files = glob.glob(f"{root_path}/total_data/{participant}-*/{sensor.lower()}.csv")
        for file in files:
            times = []
            df = pd.read_csv(file, names=["DATETIME", sensor], header=None)
            for j, row in df.iterrows():
                times.append(datetime.fromtimestamp(row['DATETIME']))

            df['DATETIME'] = times
            df['DATE'] = pd.to_datetime(df['DATETIME']).dt.date
            df['TIME'] = pd.to_datetime(df['DATETIME']).dt.time
            tmp_col = df.pop('DATE')
            df.insert(1, 'DATE', tmp_col)
            tmp_col = df.pop('TIME')
            df.insert(2, 'TIME', tmp_col)
            df = df[df[sensor] != " [Alt] + o"]
            dataframe.append(df)

        dataframe = pd.concat(dataframe).sort_values(by="DATETIME").reset_index(drop=True)

        game_log_files = os.listdir(os.path.join(root_path, f'pre-collected_user_data_csv/p1/features'))
        for file in game_log_files:
            time = file.split('_')[-1].split('-')[-1]

        dataframe.to_csv("./data/{}/".format(sensor)+participant+".csv", index=False)


if __name__ == "__main__":
    test()