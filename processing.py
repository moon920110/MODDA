import glob
import math
import os
from datetime import datetime, timedelta

import numpy as np
import pandas as pd


def main():
    num = np.arange(1, 43, 1)
    sensor = 'BVP'
    root_path = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection'

    if not os.path.exists(f'./data/{sensor}'):
        os.mkdir(f'./data/{sensor}')

    for number in range(1, num.shape[0]+1):
        dataframe = []
        participant = f"p{number}"

        if not os.path.exists(f'./data/{sensor}/{participant}'):
            os.mkdir(f'./data/{sensor}/{participant}')

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

        game_log_files = os.listdir(os.path.join(root_path, f'pre-collected_user_data_csv/{participant}/features'))

        for file in game_log_files:
            if '1P' in file:
                game_datetime = file.split('_')[-1][:-9].split('-')
                game_date = game_datetime[0].split('.')
                game_time = game_datetime[1].split('.')
                start_datetime = datetime(int(game_date[0]), int(game_date[1]), int(game_date[2]),
                                          int(game_time[0]), int(game_time[1]), int(game_time[2]))
                log_df = pd.read_csv(os.path.join(root_path,
                                                  f'pre-collected_user_data_csv/{participant}/features',
                                                  file))
                elapsed_time = math.ceil(log_df['elapsed_milli_time'].iloc[-1] / 1000.)
                end_datetime = start_datetime + timedelta(seconds=elapsed_time)

                sensor_df_per_game = dataframe[(start_datetime < pd.to_datetime(dataframe['DATETIME'])) &
                                               (pd.to_datetime(dataframe['DATETIME']) < end_datetime)]
                sensor_df_per_game.to_csv(f"./data/{sensor}/{participant}/{file}", index=False)
        # dataframe.to_csv("./data/{}/".format(sensor)+participant+".csv", index=False)


if __name__ == "__main__":
    main()