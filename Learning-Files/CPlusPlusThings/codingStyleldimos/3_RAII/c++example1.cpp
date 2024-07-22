#include <iostream>
#include <vector>
//#include <cstring>
#include <fstream>
#include <string>

using namespace std;

vector<string> read_lines_from_file(string& fileName)
{
    vector<string> lines;
    string line;
    ifstream file_handle(fileName.c_str());

    while(file_handle.good()&&!file_handle.eof()&&file_handle.peek()!=EOF){
        getline(file_handle,line);
        lines.push_back(line);
    }
    file_handle.close();
    return lines;
}

vector<string>* read_lines_from_file1(string& fileName)
{
    vector<string>* lines;
    string line;
    ifstream file_handle(fileName.c_str());
    while(file_handle.good()&&!file_handle.eof()&&file_handle.peek()!=EOF)
    {
        getline(file_handle,line);
        lines->push_back(line);
    }
    return lines;
}

vector<string>* read_lines_from_file1_1(string& fileName)
{
    vector<string>* lines=new vector<string>;
    string line;
    ifstream file_handle(fileName.c_str());

    while(file_handle.good()&&!file_handle.eof()&&file_handle.peek()!=EOF)
    {
        getline(file_handle,line);
        lines->push_back(line);
    }
    file_handle.close();
    return lines;
}

int main()
{
    string fileName1("./1.txt");
    int cout=read_lines_from_file(fileName1).size();
    std::cout<< "File1 "<<fileName1<<" contains "<<cout<<"lines.\n";
    // string fileName2("./1.txt");
    // int cout1=read_lines_from_file1(fileName2)->size();
    // std::cout<< "File2 "<<fileName2<<" contains "<<cout<<"lines.\n";
    string fileName3("./1.txt");
    vector<string>* lines=read_lines_from_file1_1(fileName3);
    int cout2=lines->size();
    std::cout<< "File3 "<<fileName3<<" contains "<<cout<<"lines.\n";
    delete lines;

    
    

    return 0;

}

