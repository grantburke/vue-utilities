<template>
  <div>
    <h1>Fetching: {{ fetching }}</h1>
    <h1>Error: {{ error }}</h1>
    <table>
      <thead>
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Address</th>
          <th>City</th>
          <th>State</th>
          <th>Zip</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="u in users" :key="u.id">
          <td>{{ u.firstName }}</td>
          <td>{{ u.lastName }}</td>
          <td>{{ u.address }}</td>
          <td>{{ u.city }}</td>
          <td>{{ u.state }}</td>
          <td>{{ u.zip }}</td>
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
          | <input type="text" v-model="search" placeholder="Filter..." />
        </div>
      </tfooter>
    </table>
  </div>
</template>

<script>
import { ref, watch, computed } from "vue";
// import useFetch from "./use/useFetch";
import useAxios from "./use/useAxios";
export default {
  setup() {
    let breweries = [];
    let users = [];
    const page = ref(1);
    const limit = ref(10);

    const search = ref("");
    const searchUrl = computed(() => encodeURI(search.value));

    // const { response, error, fetching, fetchData } = useFetch();
    // watch(() => {
    //   fetchData(
    //     `users?page=${page.value}&per_page=${limit.value}&search=${search.value}`,
    //     {});
    //   users = response;
    // });
    const { response, error, fetching, fetchData } = useAxios(
      "http://localhost:5000/api"
    );
    watch(() => {
      fetchData({
        method: "get",
        url: `/users?page=${page.value}&per_page=${limit.value}&search=${search.value}`,
      });
      users = response;
    });

    const nextPage = () => (page.value += 1);
    const prevPage = () => (page.value -= 1);
    return {
      users,
      error,
      fetching,
      nextPage,
      prevPage,
      page,
      limit,
      search,
    };
  },
};
</script>

<style scoped>
h1 {
  font-family: Arial, Helvetica, sans-serif;
}
</style>
