<script setup>
import { reactive, ref } from 'vue'
import { postCurriculo } from '@/services/curriculo'

const form = reactive({
  nome: '',
  email: '',
  telefone: '',
  cargo: '',
  escolaridade: '',
  observacoes: '',
  arquivo: null,
  dataHora: ''
})

const errors = reactive({
  nome: false,
  email: false,
  telefone: false,
  cargo: false,
  escolaridade: false,
  arquivo: false
})

const submitted = ref(false)

function handleFileUpload(event) {
  form.arquivo = event.target.files[0]
  errors.arquivo = false
}

function validateForm() {
  errors.nome = !form.nome
  errors.email = !form.email
  errors.telefone = !form.telefone
  errors.cargo = !form.cargo
  errors.escolaridade = !form.escolaridade
  errors.arquivo = !form.arquivo
  return !Object.values(errors).some(e => e)
}

async function submitForm() {
  if (!validateForm()) {
    alert('Preencha todos os campos obrigatórios.')
    return
  }

  try {
    const formData = new FormData()
    formData.append('nome', form.nome)
    formData.append('email', form.email)
    formData.append('telefone', form.telefone)
    formData.append('cargodesejado', form.cargo)
    formData.append('escolaridade', form.escolaridade)
    formData.append('observacao', form.observacoes)
    formData.append('arquivo', form.arquivo)

    await postCurriculo(formData)

    submitted.value = true
    Object.keys(form).forEach(key => form[key] = key === 'arquivo' ? null : '')
    alert("Currículo enviado com sucesso!")
  } catch (error) {
    console.error('Erro ao enviar currículo:', error)
    alert('Ocorreu um erro ao enviar o currículo.')
  }
}
</script>

<template>
  <div class="max-w-lg mx-auto p-6 bg-white shadow-md rounded-md mt-10 border border-gray-200">
    <h1 class="text-2xl font-bold mb-2">Cadastrar Currículo</h1>
    <RouterLink to="/curriculos" class="text-blue-500 underline hover:text-blue-700 mb-3">
      Ver currículos cadastrados
    </RouterLink>
    <form @submit.prevent="submitForm" class="space-y-4">
      <div>
        <label class="block font-medium mb-1">Nome <span class="text-red-500">*</span></label>
        <input type="text" v-model="form.nome" :class="['w-full border rounded px-3 py-2', errors.nome ? 'border-red-500' : 'border-gray-400']"/>
      </div>

      <div>
        <label class="block font-medium mb-1">E-mail <span class="text-red-500">*</span></label>
        <input type="email" v-model="form.email" :class="['w-full border rounded px-3 py-2', errors.email ? 'border-red-500' : 'border-gray-400']"/>
      </div>

      <div>
        <label class="block font-medium mb-1">Telefone <span class="text-red-500">*</span></label>
        <input type="tel" v-model="form.telefone" :class="['w-full border rounded px-3 py-2', errors.telefone ? 'border-red-500' : 'border-gray-400']"/>
      </div>

      <div>
        <label class="block font-medium mb-1">Cargo Desejado <span class="text-red-500">*</span></label>
        <input type="text" v-model="form.cargo" :class="['w-full border rounded px-3 py-2', errors.cargo ? 'border-red-500' : 'border-gray-400']"/>
      </div>

      <div>
        <label class="block font-medium mb-1">Escolaridade <span class="text-red-500">*</span></label>
        <select v-model="form.escolaridade" :class="['w-full border rounded px-3 py-2', errors.escolaridade ? 'border-red-500' : 'border-gray-400']">
          <option value="">Selecione</option>
          <option value="ensino-fundamental">Ensino Fundamental</option>
          <option value="ensino-medio">Ensino Médio</option>
          <option value="ensino-superior">Ensino Superior</option>
          <option value="pos-graduacao">Pós-Graduação</option>
        </select>
      </div>

      <div>
        <label class="block font-medium mb-1">Observações</label>
        <textarea v-model="form.observacoes" class="w-full border rounded px-3 py-2 border-gray-400" rows="3"></textarea>
      </div>

      <div>
        <label class="block font-medium mb-1">Arquivo <span class="text-red-500">*</span></label>
        <label for="file" :class="['flex items-center gap-2 px-4 py-2 border rounded cursor-pointer hover:bg-gray-100 transition-colors', errors.arquivo ? 'border-red-500' : 'border-gray-400']">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6 text-blue-500">
            <path stroke-linecap="round" stroke-linejoin="round" d="M12 16.5V9.75m0 0 3 3m-3-3-3 3M6.75 19.5a4.5 4.5 0 0 1-1.41-8.775 5.25 5.25 0 0 1 10.233-2.33 3 3 0 0 1 3.758 3.848A3.752 3.752 0 0 1 18 19.5H6.75Z"/>
          </svg>
          <span class="text-gray-700">Escolher arquivo</span>
        </label>
        <span class="text-sm">
            (.doc, .docx ou .pdf)
        </span>
        <input id="file" type="file" @change="handleFileUpload" class="hidden" accept=".pdf,.doc,.docx"/>
        <p v-if="form.arquivo" class="mt-2 text-sm text-gray-600">Arquivo selecionado: <span class="font-medium">{{ form.arquivo.name }}</span></p>
      </div>

      <button type="submit" class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">Enviar Currículo</button>
    </form>
  </div>
</template>
