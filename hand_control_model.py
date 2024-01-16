import cv2
import mediapipe as mp
import keyboard
import time
import numpy as np
import math
class Hand:
    def __init__(self):
        # 初始化medialpipe
        self.mp_drawing = mp.solutions.drawing_utils
        self.mp_drawing_styles = mp.solutions.drawing_styles
        self.mp_hands = mp.solutions.hands
        self.prev_thumb_x = {'left': None, 'right': None}
        self.prev_wrist_x = None
        self.left_threshold = 0.8
        self.right_threshold = 0.2
        self.movement_threshold = 0.01  # Threshold for detecting significant movement
        self.extreme_position_held = {'left': False, 'right': False}
    def hand_type(self, handedness):
        return 'left' if handedness.classification[0].label == 'Left' else 'right'

    def process_hand(self, hand_landmarks, hand_type):
        wrist_x = hand_landmarks.landmark[self.mp_hands.HandLandmark.WRIST].x
        thumb_tip_x = hand_landmarks.landmark[self.mp_hands.HandLandmark.THUMB_TIP].x
        thumb_tip_y = hand_landmarks.landmark[self.mp_hands.HandLandmark.THUMB_TIP].y
        index_pip_x = hand_landmarks.landmark[self.mp_hands.HandLandmark.INDEX_FINGER_PIP].x
        index_pip_y = hand_landmarks.landmark[self.mp_hands.HandLandmark.INDEX_FINGER_PIP].y
        x_distance = thumb_tip_x - wrist_x

        if hand_type == 'right':
            control_keys = ('a', 'd')
            self.left_threshold = -0.15
            self.right_threshold = -0.05
        else:
            control_keys = ('j', 'l')
            self.left_threshold = -0.015
            self.right_threshold = 0.13
        if math.sqrt((thumb_tip_x-index_pip_x)**2+(thumb_tip_y-index_pip_y)**2)<0.05 and hand_type == 'left':
            keyboard.send('k')
        if math.sqrt((thumb_tip_x-index_pip_x)**2+(thumb_tip_y-index_pip_y)**2)<0.05 and hand_type == 'right':
            keyboard.send('p')
        if x_distance < self.left_threshold:
            if not self.extreme_position_held[hand_type]:
                keyboard.press(control_keys[0])
                self.extreme_position_held[hand_type] = True
        elif x_distance > self.right_threshold:
            if not self.extreme_position_held[hand_type]:
                keyboard.press(control_keys[1])
                self.extreme_position_held[hand_type] = True
        else:
            if self.extreme_position_held[hand_type]:
                keyboard.release(control_keys[0])
                keyboard.release(control_keys[1])
                self.extreme_position_held[hand_type] = False
            elif self.prev_thumb_x[hand_type] is not None:
                dx = thumb_tip_x - self.prev_thumb_x[hand_type]
                if dx < -self.movement_threshold:
                    keyboard.press_and_release(control_keys[0])
                    time.sleep(0.1)
                    keyboard.press_and_release(control_keys[0])
                elif dx > self.movement_threshold:
                    keyboard.press_and_release(control_keys[1])
                    time.sleep(0.1)
                    keyboard.press_and_release(control_keys[1])
        self.prev_thumb_x[hand_type] = thumb_tip_x
    # 主函数
    def recognize(self):
        # 计算刷新率
        # fpsTime = time.time()
        # OpenCV读取视频流
        cap = cv2.VideoCapture(0)
        # 视频分辨率
        resize_w = 640
        resize_h = 480

        with self.mp_hands.Hands(min_detection_confidence=0.7,
                                 min_tracking_confidence=0.5,
                                 max_num_hands=2) as hands:
            while cap.isOpened():
                success, image = cap.read()
                image = cv2.resize(image, (resize_w, resize_h))
                if not success:
                    print("空帧.")
                    continue

                # 提高性能
                image.flags.writeable = False
                # 转为RGB
                image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
                # 镜像
                image = cv2.flip(image, 1)
                # mediapipe模型处理
                results = hands.process(image)

                image.flags.writeable = True
                image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
                # 判断是否有手掌
                if results.multi_hand_landmarks:
                    # 遍历每个手掌
                    i = 0
                    for hand_landmarks in results.multi_hand_landmarks:
                        # 在画面标注手指
                        handedness=results.multi_handedness[i]
                        self.mp_drawing.draw_landmarks(
                            image,
                            hand_landmarks,
                            self.mp_hands.HAND_CONNECTIONS,
                            self.mp_drawing_styles.get_default_hand_landmarks_style(),
                            self.mp_drawing_styles.get_default_hand_connections_style())
                        hand_type = self.hand_type(handedness)
                        self.process_hand(hand_landmarks, hand_type)
                        i+=1
                # 显示画面
                cv2.imshow('MediaPipe Hands', image)
                if cv2.waitKey(5) & 0xFF == 27 or cv2.getWindowProperty('MediaPipe Hands', cv2.WND_PROP_VISIBLE) < 1:
                    break
            cap.release()


# 开始程序
control = Hand()
control.recognize()