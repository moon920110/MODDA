import os
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt


def analyze_game_log():
    log_path = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection\\pre-collected_user_data_csv\\'
    players = os.listdir(log_path)

    for player in players:
        path = os.path.join(log_path, player, 'features')
        files = os.listdir(path)
        for file in files:
            df = pd.read_csv(os.path.join(path, file))
            sns.lineplot(data=df, x='elapsed_milli_time', y='hp_diff')
            plt.show()
            print(df['hp_diff'].mean())


if __name__ == '__main__':
    analyze_game_log()