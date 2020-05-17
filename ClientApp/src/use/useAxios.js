import { toRefs, reactive } from "vue";
import axios from "axios";

export default function useFetch(baseURL) {
  const axiosClient = axios.create({ baseURL });
  const state = reactive({
    response: [],
    error: null,
    fetching: false,
  });

  const fetchData = async (config) => {
    try {
      state.fetching = true;
      const res = await axiosClient(config);
      state.response = res.data;
    } catch (error) {
      state.error = error;
    } finally {
      state.fetching = false;
    }
  };

  return { ...toRefs(state), fetchData };
}
