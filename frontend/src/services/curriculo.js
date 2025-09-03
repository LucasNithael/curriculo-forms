import api from './base'

export const getCurriculo = async () => {
  try {
    const response = await api.get('Curriculo')
    return response
  } catch (error) {
    throw error
  }
}

export const postCurriculo = async (formData) => {
    try {
        const response = await api.post('Curriculo', formData, {
            headers: {
            'Content-Type': 'multipart/form-data'
            }
        })
    return response.data
    } catch (error) {
        throw error
    }
}
