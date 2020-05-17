<template>
  <div>
    <h1>Fetching: {{ fetching }}</h1>
    <h1>Error: {{ error }}</h1>
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Type</th>
          <th>City</th>
          <th>State</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="b in breweries" :key="b.id">
          <td>{{ b.name }}</td>
          <td>{{ b.brewery_type }}</td>
          <td>{{ b.city }}</td>
          <td>{{ b.state }}</td>
        </tr>
      </tbody>
      <tfooter id="pagination">
        <button type="button" @click="prevPage">Prev</button>
        <button type="button" @click="nextPage">Next</button>
        <div>
          Page: {{ page }}
          | Limit
          <select v-model="limit">
            <option value="10" selected>10</option>
            <option value="25">25</option>
            <option value="50">50</option>
          </select>
          | <input type="text" v-model="city" placeholder="Filter by city..." />
        </div>
      </tfooter>
    </table>
  </div>
</template>

<script>
import { ref, watch, computed } from "vue";
import useFetch from "./use/useFetch";
import useAxios from "./use/useAxios";
export default {
  setup() {
    let breweries = [];
    const page = ref(1);
    const limit = ref(10);

    const city = ref("");
    const cityUrl = computed(() => encodeURI(city.value));

    // const { response, error, fetching, fetchData } = useFetch();
    const { response, error, fetching, fetchData } = useAxios(
      "https://api.openbrewerydb.org"
    );
    watch(() => {
      fetchData({
        method: "get",
        url: `/breweries?page=${page.value}&per_page=${limit.value}&by_city=${city.value}`,
      });
      breweries = response;
    });

    const nextPage = () => (page.value += 1);
    const prevPage = () => (page.value -= 1);
    return {
      breweries,
      error,
      fetching,
      nextPage,
      prevPage,
      page,
      limit,
      city,
    };
  },
};
</script>

<style scoped>
h1 {
  font-family: Arial, Helvetica, sans-serif;
}
</style>
