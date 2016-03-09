#!/usr/bin/env python
import fnmatch
import os
from xml.dom import minidom
from xml.etree import cElementTree as ElementTree

class bcolors:
    FAIL = '\033[91m'
    ENDC = '\033[0m'

def prettify(tree):
    string = ElementTree.tostring(tree.getroot(), encoding="UTF-8")
    reparsed = minidom.parseString(string)
    pretty = '\n'.join([line for line in reparsed.toprettyxml(indent=' '*2, encoding="UTF-8").decode("UTF-8").split('\n') if line.strip()])
    tree._setroot(ElementTree.fromstring(pretty))

def getFiles():
    files = []
    for root, dirnames, filenames in os.walk('.'):
        if root != ".":
            for filename in fnmatch.filter(filenames, '*.xml'):
                files.append(os.path.join(root, filename))
    return files

def isValid(tree):
    try:
        root = tree.getroot()[0]
        if root.find("Name").text == "Custom":
            print(bcolors.FAIL + "\tThe name of the preset can't be \"Custom\"" + bcolors.ENDC)
            return False
    except:
        return False
    return True

if __name__ == "__main__":    
    fileOut = "LiveSplit.Skyrim.Presets.xml"
    if os.path.isfile(fileOut):
        print("Deleting \"" + fileOut + "\"")
        os.remove(fileOut)
    tree = None
    for filename in getFiles():
        print("Adding \"" + filename + "\"...")
        data = ElementTree.parse(filename)
        if not isValid(data):
            print("\""+ filename +"\" is invalid. Aborting.")
            exit(1)
        if tree is None:
            tree = data
        else:
            tree.getroot().extend(data.getroot())            
    if tree is not None:
        prettify(tree)
        print("Writing to " + fileOut + "...")
        tree.write(fileOut, encoding="UTF-8", xml_declaration=True)
        print("Done.")
    else:
        print("Error.")
        exit(1)
