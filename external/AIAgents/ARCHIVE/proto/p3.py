import tkinter as tk
from tkinter import filedialog

# Variables to store the mouse's initial click position
start_x, start_y = 0, 0

# Function to handle the mouse button press event
def on_drag_start(event):
    global start_x, start_y
    start_x = event.x
    start_y = event.y

# Function to handle the mouse motion event (dragging)
def on_drag_motion(event):
    x, y = event.x, event.y
    delta_x = x - start_x
    delta_y = y - start_y
    new_x = root.winfo_x() + delta_x
    new_y = root.winfo_y() + delta_y
    root.geometry(f"+{new_x}+{new_y}")

# Function to handle the mouse button release event
def on_drag_release(event):
    pass

# Function to handle button clicks
def button_click(button_num):
    if button_num == 1:
        # Handle the first button click action here
        print("Button 1 clicked")
    elif button_num == 2:
        # Handle the second button click action here
        print("Button 2 clicked")
    elif button_num == 3:
        # Handle the third button click action here
        print("Button 3 clicked")

# Function to open a file dialog
def open_file_dialog():
    file_path = filedialog.askopenfilename()
    if file_path:
        print("Selected file:", file_path)

# Create the main application window
root = tk.Tk()
root.title("Python Form")

# Make the window frame draggable
root.bind("<ButtonPress-1>", on_drag_start)
root.bind("<B1-Motion>", on_drag_motion)
root.bind("<ButtonRelease-1>", on_drag_release)

# Create a label
label = tk.Label(root, text="UberGeek", font=("Arial", 18))
label.pack()

# Create buttons
button1 = tk.Button(root, text="Create Agent", command=lambda: button_click(1))
button2 = tk.Button(root, text="Delete Agent", command=lambda: button_click(2))
button3 = tk.Button(root, text="Button 3", command=lambda: button_click(3))

# Create a file selector button
file_button = tk.Button(root, text="Upload File", command=open_file_dialog)

# Place buttons in the window
button1.pack()
button2.pack()
button3.pack()
file_button.pack()

# Start the main event loop
root.mainloop()
