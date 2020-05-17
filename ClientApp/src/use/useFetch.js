import { toRefs, reactive } from "vue";

export default function useFetch() {
  const baseUrl = "http://localhost:5000/api";
  const state = reactive({
    response: [],
    error: null,
    fetching: false,
  });

  const fetchData = async (route, config) => {
    try {
      state.fetching = true;
      const res = await fetch(`${baseUrl}/${route}`, config);
      const json = await res.json();
      state.response = json;
    } catch (error) {
      state.error = error;
    } finally {
      state.fetching = false;
    }
  };

  return { ...toRefs(state), fetchData };
}
