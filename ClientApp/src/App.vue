<template>
  <div>
    <h1>Fetching: {{ fetching }}</h1>
    <h1>Error: {{ error }}</h1>
    <table>
      <thead>
        <tr>
          <th @click="sort('firstName')">First Name</th>
          <th @click="sort('lastName')">Last Name</th>
          <th @click="sort('address')">Address</th>
          <th @click="sort('city')">City</th>
          <th @click="sort('state')">State</th>
          <th @click="sort('zip')">Zip</th>
        </tr>
      </thead>
      <tbody v-if="usersInfo">
        <tr v-for="u in usersInfo.users" :key="u.id">
          <td>{{ u.firstName }}</td>
          <td>{{ u.lastName }}</td>
          <td>{{ u.address }}</td>
          <td>{{ u.city }}</td>
          <td>{{ u.state }}</td>
          <td>{{ u.zip }}</td>
        </tr>
      </tbody>
      <tfooter id="pagination">
        <button type="button" @click="prevPage" :disabled="disablePrev">
          Prev
        </button>
        |
        <button
          type="button"
          v-for="n in btnPageNumbers"
          :key="n"
          @click="goToPage(n)"
        >
          {{ n }}
        </button>
        |
        <button type="button" @click="nextPage" :disabled="disableNext">
          Next
        </button>
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
import { ref, watch, computed, reactive } from "vue";
// import useFetch from "./use/useFetch";
import useAxios from "./use/useAxios";
export default {
  setup() {
    let usersInfo = reactive(null);
    const page = ref(1);
    const limit = ref(10);
    const totalPages = computed(
      () => usersInfo.value.usersTotal / limit.value || 0
    );
    const btnPageNumbers = computed(() => {
      let total = usersInfo.value.usersTotal / limit.value || 0;
      let start = 1;
      let end = total;

      if (page.value - 2 <= 0) start = 1;
      else start = page.value - 2;

      if (start + 4 > total) end = total;
      else end = start + 4;
      return Array.from({ length: end - start + 1 }, (_, i) => start + i);
    });

    const search = ref("");
    const searchUrl = computed(() => encodeURI(search.value));
    const sortColumn = ref("");
    const sortDirection = ref("");

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
        url: `/users?page=${page.value}&per_page=${limit.value}&search=${search.value}&sort_column=${sortColumn.value}&sort_direction=${sortDirection.value}`,
      });
      usersInfo = response;
    });

    const sort = (column) => {
      if (column == sortColumn.value)
        sortDirection.value =
          sortDirection.value == "ascending" ? "descending" : "ascending";
      else sortDirection.value = "ascending";
      sortColumn.value = column;
    };
    const nextPage = () => (page.value += 1);
    const disableNext = computed(() => page.value + 1 > totalPages.value);
    const prevPage = () => (page.value -= 1);
    const disablePrev = computed(() => page.value - 1 <= 0);
    const goToPage = (n) => (page.value = n);
    return {
      usersInfo,
      error,
      fetching,
      sort,
      nextPage,
      disableNext,
      prevPage,
      disablePrev,
      goToPage,
      btnPageNumbers,
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
