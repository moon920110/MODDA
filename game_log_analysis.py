import os
from glob import glob
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt


def analyze_game_log():
    root_path = '/Users/supermoon/Documents/Research'
    log_path = 'DDA/data/important_dataset/June_21_data_collection/pre-collected_user_data_csv/'
    log_path = os.path.join(root_path, log_path)
    gsr_path = 'DDA_following/data/GSR_peaks_from_pre_collection/'
    gsr_path = os.path.join(root_path, gsr_path)

    dirs = os.listdir(gsr_path)

    for d in dirs:
        if d != '.DS_Store':
            player = d.split('_')[0]
            path = os.path.join(log_path, player, 'features')
            files = os.listdir(path)
            for file in files:
                try:
                    gsr_df = pd.read_csv(os.path.join(gsr_path, d, file))
                    log_df = pd.read_csv(os.path.join(path, file))
                    log_len = log_df.shape[0]
                    cda_df = pd.read_excel(glob(os.path.join(gsr_path, d, f'*{file[:-4]}*xls'))[0], sheet_name=None)['CDA']
                    print(cda_df)

                    gsr = gsr_df.loc[gsr_df.index.repeat(15)].reset_index(drop=True)['GSR']
                    log_df['GSR'] = gsr.iloc[:log_len]

                except BaseException as e:
                    print(e)
                # sns.lineplot(data=df, x='elapsed_milli_time', y='hp_diff')
                # plt.show()
                # print(df['hp_diff'].mean())


if __name__ == '__main__':
    analyze_game_log()