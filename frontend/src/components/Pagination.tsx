import { useNavigate } from "react-router-dom";

type PaginationProps = {
  baseUrl: string;
  currentPage: number;
  perPage: number;
  total: number;
};

export default function Pagination({
  baseUrl,
  currentPage,
  perPage,
  total,
}: PaginationProps) {
  const navigate = useNavigate();

  const totalPages = Math.ceil(total / perPage);

  // if (totalPages == 1) return null;

  function navigateToPage(page: number) {
    navigate(`${baseUrl}?page=${page}&perPage=${perPage}`);
  }

  console.log("currentPage", currentPage);
  console.log("totalPages", totalPages);
  console.log("total", total);
  console.log("perPage", perPage);
  console.log("baseUrl", baseUrl);

  return (
    <div className="flex gap-1 border justify-center items-center w-fit">
      <button onClick={() => navigateToPage(currentPage - 1)}>Previous</button>
      <button onClick={() => navigateToPage(currentPage + 1)}>Next</button>
    </div>
  );
}
