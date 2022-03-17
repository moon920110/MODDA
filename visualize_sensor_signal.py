import os
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt


sensor_path = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection\\GSR'
log_file = 'D:\\Research\\Game\\DDA\\data\\important_dataset\\June_21_data_collection\\pre-collected_user_data_csv\\p1\\features\\HPMode_KeyBoard_KeyBoard_2021.06.03-16.02.51.json.csv'
sensor_files = os.listdir(sensor_path)


for file in sensor_files:
    if 'csv' in file:
        player = file.split('.')[0]
        df = pd.read_csv(os.path.join(sensor_path, file))
        print(df['TIME'])
        # ax = sns.lineplot(data=df, x='TIME', y='GSR')
        # plt.show()
        break