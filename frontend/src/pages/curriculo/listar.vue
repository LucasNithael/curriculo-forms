<script setup>
import { ref, onMounted } from 'vue'
import { getCurriculo } from '@/services/curriculo'

const curriculos = ref([])
const loading = ref(false)
const error = ref(null)

async function fetchCurriculos() {
  loading.value = true
  error.value = null
  try {
    const response = await getCurriculo()
    curriculos.value = response.data.data.map(c => ({
      ...c,
      arquivoUrl: c.arquivo ? criarUrlArquivo(c.arquivo, c.nomeArquivo) : null
    }))
  } catch (err) {
    console.error('Erro ao buscar currículos:', err)
    error.value = 'Erro ao carregar currículos.'
  } finally {
    loading.value = false
  }
}

function criarUrlArquivo(base64) {
  const binaryString = atob(base64)
  const len = binaryString.length
  const bytes = new Uint8Array(len)
  for (let i = 0; i < len; i++) {
    bytes[i] = binaryString.charCodeAt(i)
  }
  const blob = new Blob([bytes])
  return URL.createObjectURL(blob)
}


onMounted(() => {
  fetchCurriculos()
})
</script>

<template>
  <div class="max-w-4xl mx-auto p-6 bg-white shadow-md rounded-md mt-10 border border-gray-200">
    <h1 class="text-2xl font-bold mb-4">Listagem de Currículos</h1>
    <RouterLink to="/" class="text-blue-500 underline hover:text-blue-700 mb-3 inline-block">
      Cadastrar novo currículo
    </RouterLink>

    <div v-if="loading" class="text-gray-500">Carregando...</div>
    <div v-if="error" class="text-red-500">{{ error }}</div>

    <table v-if="curriculos.length && !loading" class="w-full table-auto border-collapse">
      <thead>
        <tr class="bg-gray-100">
          <th class="border px-4 py-2 text-left">Nome</th>
          <th class="border px-4 py-2 text-left">E-mail</th>
          <th class="border px-4 py-2 text-left">Telefone</th>
          <th class="border px-4 py-2 text-left">Cargo</th>
          <th class="border px-4 py-2 text-left">Escolaridade</th>
          <th class="border px-4 py-2 text-left">Arquivo</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="curriculo in curriculos" :key="curriculo.id">
          <td class="border px-4 py-2">{{ curriculo.nome }}</td>
          <td class="border px-4 py-2">{{ curriculo.email }}</td>
          <td class="border px-4 py-2">{{ curriculo.telefone }}</td>
          <td class="border px-4 py-2">{{ curriculo.cargoDesejado }}</td>
          <td class="border px-4 py-2">{{ curriculo.escolaridade }}</td>
          <td class="border px-4 py-2">
            <a 
              v-if="curriculo.arquivoUrl"
              :href="curriculo.arquivoUrl" 
              :download="curriculo.nomeArquivo || 'arquivo'"
              class="text-blue-500 underline hover:text-blue-700"
            >
              Download
            </a>
            <span v-else class="text-gray-500">Sem arquivo</span>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="!curriculos.length && !loading" class="text-gray-500 mt-4">
      Nenhum currículo cadastrado.
    </div>
  </div>
</template>
