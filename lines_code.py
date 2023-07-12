import os

count = 0
max = 0
maxFile = ""
fileLengh = 0
# get list of files in lib directory
for root, directories, files in os.walk("./raya", topdown=False):
    files = [f for f in files if f.endswith(
        '.cs') and not f[0].isdigit() and 'Migrations' not in root and 'bin' not in root]
    for name in files:
        if name.endswith(".cs") and not name[0].isdigit():
            fileLengh += 1
            file = open(os.path.join(root, name), encoding="utf8")
            # get line of file
            lineFile = 0
            for line in file:
                if line != "\n":
                    count += 1
                    lineFile += 1
            if lineFile > max:
                max = lineFile
                maxFile = name
            file.close()
print("Number of lines of code:", count)
print("Number of Files:", fileLengh)
print("File with most lines of code:", maxFile)
print("Number of lines of code in file with most lines of code:", max)
add_str = f"""
## Count lines of codes: {count}
## Number of Files:: {fileLengh}
## File with most lines of code: {maxFile}
## Most lines of code in one file: {max}
"""


def prepend_line():
    # define name of temporary dummy file
    dummy_file = "README.md" + '.bak'
    # open original file in read mode and dummy file in write mode
    with open("README.md", 'r') as read_obj, open(dummy_file, 'w') as write_obj:
        # Write given line to the dummy file
        write_obj.write(add_str + '\n')
        # Read lines from original file one by one and append them to the dummy file
        for line in read_obj:
            write_obj.write(line)
    os.remove("README.md")
    # Rename dummy file as the original file
    os.rename(dummy_file, "README.md")


prepend_line()
