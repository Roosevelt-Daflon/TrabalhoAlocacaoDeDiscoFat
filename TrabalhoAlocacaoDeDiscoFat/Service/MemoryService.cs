using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TrabalhoAlocacaoDeDiscoFat.Models;

namespace TrabalhoAlocacaoDeDiscoFat.Service
{
    public struct MemoryService
    {
        private string[] Memory = new string[10];
        private List<(int nextIndex, int DataIndex)> Fat = new();
        private List<Arquivo> Arquivos = new();
        public MemoryService()
        {
            for (int i = 0; i < Memory.Length; i++) Memory[i] = "";

        }

        public string[] GetMemory() => Memory;
        public List<(int nextIndex, int DataIndex)> GetFat() => Fat;
        public List<Arquivo> GetArquivos() => Arquivos;

        public void ExcluirArquivo(string name) 
        {
            var arquivo = Arquivos.FirstOrDefault(x => x.Name == name);
            Arquivos.Remove(arquivo);
        }

        public Arquivo GetArquivo(string name)
        {
            return Arquivos.FirstOrDefault(x => x.Name == name);
        }

        public void SetMemory(int index, string data)
        {
            Memory[index] = data;
        }

        public int AddData(List<int> indexs, string file)
        {
            var nextIndex = -1;

            for (int i =0; i < indexs.Count; i++)
            {
                Memory[indexs[i]] = file;
                Fat.Add((nextIndex, indexs[i]));
                nextIndex = Fat.IndexOf(Fat.Last());
            }

            return nextIndex;
        }

        public void AddArquivo(string nome, int tamanho, int startIndex)
        {
            Arquivos.Add(new()
            {
                Name = nome,
                Space = tamanho,
                StartIndex = startIndex
            });
        }

        public void RemoveData(int startIndex)
        {
            
            while (startIndex != -1)
            {
                (int nextIndex, int DataIndex) = Fat[startIndex];
                Memory[DataIndex] = "";
                Fat.Remove(Fat[startIndex]);
                startIndex = nextIndex;
            }
        }

        public List<int>? GetFreeIndex(int length)
        {
            var indexs = new List<int>();


            for (int i = 0; i < Memory.Length; i++)
            {
                if(indexs.Count == length) break;

                if (string.IsNullOrEmpty(Memory[i])) indexs.Add(i);
            }

            if (indexs.Count < length)
                return null;

            return indexs;

        }
    }
}
