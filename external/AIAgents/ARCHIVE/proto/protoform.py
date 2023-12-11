import tkinter as tk
from tkinter import filedialog

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

# Create buttons
button1 = tk.Button(root, text="Button 1", command=lambda: button_click(1))
button2 = tk.Button(root, text="Button 2", command=lambda: button_click(2))
button3 = tk.Button(root, text="Button 3", command=lambda: button_click(3))

# Create a file selector button
file_button = tk.Button(root, text="Select File", command=open_file_dialog)

# Place buttons in the window
button1.pack()
button2.pack()
button3.pack()
file_button.pack()

# Start the main event loop
root.mainloop()